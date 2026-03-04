using System.Security.Claims;
using System.Text.Encodings.Web;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Todo.WebApi.Services.Realtime;
using Xunit;

namespace Todo.WebApi.Tests;

public sealed class ChatHubIntegrationTests(TodoWebApiFactory factory): IClassFixture<TodoWebApiFactory>
{
    [Fact]
    public async Task JoinRoom_Should_ReturnSnapshot_WithCurrentMember()
    {
        var ct = TestContext.Current.CancellationToken;
        var roomId = $"room-{Guid.NewGuid():N}";
        var token = CreateAccessToken("u-join", "join-user");
        var snapshotTask =
            new TaskCompletionSource<RoomSnapshotDto>(TaskCreationOptions.RunContinuationsAsynchronously);

        await using var connection = CreateConnection(token);
        connection.On<RoomSnapshotDto>("RoomSnapshot", snapshot => snapshotTask.TrySetResult(snapshot));

        await connection.StartAsync(ct);
        await connection.InvokeAsync("JoinRoomAsync", roomId, ct);

        var snapshot = await snapshotTask.Task.WaitAsync(TimeSpan.FromSeconds(5), ct);
        snapshot.RoomId.Should().Be(roomId);
        snapshot.Members.Should().Contain("u-join");
    }

    [Fact]
    public async Task SendRoomMessage_Should_Broadcast_ToOtherConnection()
    {
        var ct = TestContext.Current.CancellationToken;
        var roomId = $"room-{Guid.NewGuid():N}";
        var senderToken = CreateAccessToken("u-sender", "sender");
        var receiverToken = CreateAccessToken("u-receiver", "receiver");
        var messageTask = new TaskCompletionSource<ChatMessageDto>(TaskCreationOptions.RunContinuationsAsynchronously);

        await using var sender = CreateConnection(senderToken);
        await using var receiver = CreateConnection(receiverToken);

        receiver.On<ChatMessageDto>("RoomMessageReceived", message => messageTask.TrySetResult(message));

        await sender.StartAsync(ct);
        await receiver.StartAsync(ct);
        await sender.InvokeAsync("JoinRoomAsync", roomId, ct);
        await receiver.InvokeAsync("JoinRoomAsync", roomId, ct);
        await sender.InvokeAsync("SendRoomMessageAsync", roomId, "hello receiver", ct);

        var message = await messageTask.Task.WaitAsync(TimeSpan.FromSeconds(5), ct);
        message.RoomId.Should().Be(roomId);
        message.UserId.Should().Be("u-sender");
        message.Content.Should().Be("hello receiver");
    }

    [Fact]
    public async Task SyncRoomState_Should_Contain_RecentMessageHistory()
    {
        var ct = TestContext.Current.CancellationToken;
        var roomId = $"room-{Guid.NewGuid():N}";
        var writerToken = CreateAccessToken("u-writer", "writer");
        var readerToken = CreateAccessToken("u-reader", "reader");

        await using var writer = CreateConnection(writerToken);
        await writer.StartAsync(ct);
        await writer.InvokeAsync("JoinRoomAsync", roomId, ct);
        await writer.InvokeAsync("SendRoomMessageAsync", roomId, "history message", ct);

        await using var reader = CreateConnection(readerToken);
        var initialSnapshotTask =
            new TaskCompletionSource<RoomSnapshotDto>(TaskCreationOptions.RunContinuationsAsynchronously);
        var syncedSnapshotTask =
            new TaskCompletionSource<RoomSnapshotDto>(TaskCreationOptions.RunContinuationsAsynchronously);
        var snapshotCounter = 0;
        reader.On<RoomSnapshotDto>("RoomSnapshot", snapshot =>
        {
            snapshotCounter++;
            if (snapshotCounter == 1)
            {
                initialSnapshotTask.TrySetResult(snapshot);
                return;
            }

            syncedSnapshotTask.TrySetResult(snapshot);
        });

        await reader.StartAsync(ct);
        await reader.InvokeAsync("JoinRoomAsync", roomId, ct);
        await initialSnapshotTask.Task.WaitAsync(TimeSpan.FromSeconds(5), ct);

        await reader.InvokeAsync("SyncRoomStateAsync", roomId, ct);
        var syncedSnapshot = await syncedSnapshotTask.Task.WaitAsync(TimeSpan.FromSeconds(5), ct);

        syncedSnapshot.RecentMessages.Should()
            .ContainSingle(message => message.Content == "history message" && message.UserId == "u-writer");
    }

    [Fact]
    public async Task ConnectWithoutJwt_Should_FailWithUnauthorized()
    {
        var ct = TestContext.Current.CancellationToken;
        await using var connection = CreateConnection(null);
        HttpRequestException? exception = null;
        try
        {
            await connection.StartAsync(ct);
        }
        catch (HttpRequestException ex)
        {
            exception = ex;
        }

        exception.Should().NotBeNull();
        exception.Message.Should().Contain("401");
    }

    [Fact]
    public async Task Disconnect_Should_NotRemoveUser_WhenAnotherConnectionExists()
    {
        var ct = TestContext.Current.CancellationToken;
        var roomId = $"room-{Guid.NewGuid():N}";
        var sharedUserToken = CreateAccessToken("u-shared", "shared-user");
        var observerToken = CreateAccessToken("u-observer", "observer");

        await using var firstConnection = CreateConnection(sharedUserToken);
        await using var secondConnection = CreateConnection(sharedUserToken);

        var secondSnapshotTask =
            new TaskCompletionSource<RoomSnapshotDto>(TaskCreationOptions.RunContinuationsAsynchronously);
        secondConnection.On<RoomSnapshotDto>("RoomSnapshot", snapshot => secondSnapshotTask.TrySetResult(snapshot));

        await firstConnection.StartAsync(ct);
        await secondConnection.StartAsync(ct);
        await firstConnection.InvokeAsync("JoinRoomAsync", roomId, ct);
        await secondConnection.InvokeAsync("JoinRoomAsync", roomId, ct);
        await secondConnection.InvokeAsync("SyncRoomStateAsync", roomId, ct);

        var snapshotBeforeDisconnect = await secondSnapshotTask.Task.WaitAsync(TimeSpan.FromSeconds(5), ct);
        snapshotBeforeDisconnect.Members.Should().Contain("u-shared");

        await firstConnection.StopAsync(ct);
        secondSnapshotTask =
            new TaskCompletionSource<RoomSnapshotDto>(TaskCreationOptions.RunContinuationsAsynchronously);
        await secondConnection.InvokeAsync("SyncRoomStateAsync", roomId, ct);
        var snapshotAfterFirstDisconnect = await secondSnapshotTask.Task.WaitAsync(TimeSpan.FromSeconds(5), ct);
        snapshotAfterFirstDisconnect.Members.Should().Contain("u-shared");

        await secondConnection.StopAsync(ct);

        await using var observerConnection = CreateConnection(observerToken);
        var observerSnapshotTask =
            new TaskCompletionSource<RoomSnapshotDto>(TaskCreationOptions.RunContinuationsAsynchronously);
        observerConnection.On<RoomSnapshotDto>("RoomSnapshot", snapshot => observerSnapshotTask.TrySetResult(snapshot));

        await observerConnection.StartAsync(ct);
        await observerConnection.InvokeAsync("JoinRoomAsync", roomId, ct);
        var observerSnapshot = await observerSnapshotTask.Task.WaitAsync(TimeSpan.FromSeconds(5), ct);

        observerSnapshot.Members.Should().Contain("u-observer");
        observerSnapshot.Members.Should().NotContain("u-shared");
    }

    private HubConnection CreateConnection(string? accessToken)
    {
        var target = string.IsNullOrWhiteSpace(accessToken)
            ? "/hubs/chat"
            : $"/hubs/chat?access_token={Uri.EscapeDataString(accessToken)}";

        return new HubConnectionBuilder()
            .WithUrl(new Uri(factory.Server.BaseAddress, target), options =>
            {
                options.HttpMessageHandlerFactory = _ => factory.Server.CreateHandler();
                options.Transports = HttpTransportType.LongPolling;
                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    options.AccessTokenProvider = () => Task.FromResult<string?>(accessToken);
                }
            })
            .Build();
    }

    private static string CreateAccessToken(string userId, string userName)
    {
        return $"{userId}|{userName}";
    }
}

public sealed class TodoWebApiFactory: WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, configBuilder) =>
        {
            configBuilder.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["SignalR:UseRedisBackplane"] = "false"
            });
        });

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IChatRoomRedisService>();
            services.AddSingleton<IChatRoomRedisService, InMemoryChatRoomRedisService>();
            services.AddAuthentication(TestHubAuthHandler.SchemeName)
                .AddScheme<AuthenticationSchemeOptions, TestHubAuthHandler>(
                    TestHubAuthHandler.SchemeName,
                    _ => { });
            services.PostConfigure<AuthenticationOptions>(options =>
            {
                options.DefaultAuthenticateScheme = TestHubAuthHandler.SchemeName;
                options.DefaultChallengeScheme = TestHubAuthHandler.SchemeName;
                options.DefaultScheme = TestHubAuthHandler.SchemeName;
            });
        });
    }
}

internal sealed class TestHubAuthHandler: AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string SchemeName = "TestHubAuth";

    public TestHubAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var token = Request.Query["access_token"].ToString();
        if (string.IsNullOrWhiteSpace(token)
            && Request.Headers.Authorization.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            token = Request.Headers.Authorization.ToString()["Bearer ".Length..].Trim();
        }

        if (string.IsNullOrWhiteSpace(token))
        {
            return Task.FromResult(AuthenticateResult.Fail("Missing access token."));
        }

        var parts = token.Split('|', 2, StringSplitOptions.TrimEntries);
        if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid access token format."));
        }

        var userId = parts[0];
        var userName = parts[1];
        var identity = new ClaimsIdentity([
            new Claim("sub", userId),
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Email, $"{userName}@example.com")
        ], SchemeName);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, SchemeName);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}

internal sealed class InMemoryChatRoomRedisService: IChatRoomRedisService
{
    private readonly Lock gate = new();
    private readonly Dictionary<string, HashSet<string>> connectionRooms = new(StringComparer.Ordinal);
    private readonly Dictionary<string, HashSet<string>> roomMembers = new(StringComparer.Ordinal);
    private readonly Dictionary<(string RoomId, string UserId), int> roomUserConnectionCount = new();
    private readonly Dictionary<string, List<ChatMessageDto>> roomMessages = new(StringComparer.Ordinal);

    public Task<RoomJoinResult> JoinRoomAsync(
        string connectionId,
        string roomId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        lock (gate)
        {
            if (!connectionRooms.TryGetValue(connectionId, out var rooms))
            {
                rooms = new HashSet<string>(StringComparer.Ordinal);
                connectionRooms[connectionId] = rooms;
            }

            if (!rooms.Add(roomId))
            {
                return Task.FromResult(new RoomJoinResult(false, false));
            }

            var key = ( roomId, userId );
            roomUserConnectionCount.TryGetValue(key, out var userConnectionCount);
            userConnectionCount++;
            roomUserConnectionCount[key] = userConnectionCount;

            if (!roomMembers.TryGetValue(roomId, out var members))
            {
                members = new HashSet<string>(StringComparer.Ordinal);
                roomMembers[roomId] = members;
            }

            var firstConnectionInRoom = userConnectionCount == 1;
            if (firstConnectionInRoom)
            {
                members.Add(userId);
            }

            return Task.FromResult(new RoomJoinResult(true, firstConnectionInRoom));
        }
    }

    public Task<RoomLeaveResult> LeaveRoomAsync(
        string connectionId,
        string roomId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        lock (gate)
        {
            if (!connectionRooms.TryGetValue(connectionId, out var rooms) || !rooms.Remove(roomId))
            {
                return Task.FromResult(new RoomLeaveResult(false, false));
            }

            if (rooms.Count == 0)
            {
                connectionRooms.Remove(connectionId);
            }

            var key = ( roomId, userId );
            roomUserConnectionCount.TryGetValue(key, out var userConnectionCount);
            userConnectionCount--;

            var removed = userConnectionCount <= 0;
            if (removed)
            {
                roomUserConnectionCount.Remove(key);
                if (roomMembers.TryGetValue(roomId, out var members))
                {
                    members.Remove(userId);
                    if (members.Count == 0)
                    {
                        roomMembers.Remove(roomId);
                    }
                }
            }
            else
            {
                roomUserConnectionCount[key] = userConnectionCount;
            }

            return Task.FromResult(new RoomLeaveResult(true, removed));
        }
    }

    public Task<bool> IsConnectionInRoomAsync(
        string connectionId,
        string roomId,
        CancellationToken cancellationToken = default)
    {
        lock (gate)
        {
            return Task.FromResult(
                connectionRooms.TryGetValue(connectionId, out var rooms) && rooms.Contains(roomId));
        }
    }

    public Task<IReadOnlyList<string>> GetConnectionRoomsAsync(
        string connectionId,
        CancellationToken cancellationToken = default)
    {
        lock (gate)
        {
            if (!connectionRooms.TryGetValue(connectionId, out var rooms))
            {
                return Task.FromResult<IReadOnlyList<string>>([]);
            }

            return Task.FromResult<IReadOnlyList<string>>(rooms.ToArray());
        }
    }

    public Task RemoveConnectionAsync(string connectionId, CancellationToken cancellationToken = default)
    {
        lock (gate)
        {
            connectionRooms.Remove(connectionId);
        }

        return Task.CompletedTask;
    }

    public Task AppendRoomMessageAsync(
        ChatMessageDto message,
        int historyLimit = 50,
        CancellationToken cancellationToken = default)
    {
        lock (gate)
        {
            if (!roomMessages.TryGetValue(message.RoomId, out var messages))
            {
                messages = [];
                roomMessages[message.RoomId] = messages;
            }

            messages.Insert(0, message);
            if (messages.Count > historyLimit)
            {
                messages.RemoveRange(historyLimit, messages.Count - historyLimit);
            }
        }

        return Task.CompletedTask;
    }

    public Task<RoomSnapshotDto> GetRoomSnapshotAsync(
        string roomId,
        int historyLimit = 50,
        CancellationToken cancellationToken = default)
    {
        lock (gate)
        {
            var members = roomMembers.TryGetValue(roomId, out var memberSet)
                ? memberSet.OrderBy(value => value, StringComparer.Ordinal).ToArray()
                : [];

            var messages = roomMessages.TryGetValue(roomId, out var messageList)
                ? messageList.Take(historyLimit).Reverse().ToArray()
                : [];

            return Task.FromResult(
                new RoomSnapshotDto(roomId, members, messages, DateTimeOffset.UtcNow));
        }
    }
}
