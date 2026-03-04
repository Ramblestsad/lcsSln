using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Todo.WebApi.Services.Realtime;

namespace Todo.WebApi.Hubs;

[Authorize]
public class ChatHub: Hub<IChatClient>
{
    private const int MaxMessageLength = 500;
    private const int RoomHistoryLimit = 50;

    private static readonly Regex RoomIdPattern = new(
        "^[a-zA-Z0-9:_-]{3,64}$",
        RegexOptions.Compiled);

    private readonly IChatRoomRedisService chatRoomRedisService;
    private readonly ILogger<ChatHub> logger;

    public ChatHub(IChatRoomRedisService chatRoomRedisService, ILogger<ChatHub> logger)
    {
        this.chatRoomRedisService = chatRoomRedisService;
        this.logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        logger.LogInformation(
            "Chat connection established. ConnectionId: {ConnectionId}, User: {User}",
            Context.ConnectionId,
            Context.User?.Identity?.Name);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var (userId, userName) = ResolveIdentity();
        var connectionId = Context.ConnectionId;

        try
        {
            var roomIds = await chatRoomRedisService.GetConnectionRoomsAsync(connectionId);
            foreach (var roomId in roomIds)
            {
                await Groups.RemoveFromGroupAsync(connectionId, roomId);

                var leaveResult = await chatRoomRedisService.LeaveRoomAsync(connectionId, roomId, userId);
                if (!leaveResult.Left || !leaveResult.UserRemovedFromRoom)
                {
                    continue;
                }

                await Clients.Group(roomId).UserLeft(
                    new RoomUserEventDto(roomId, userId, userName, DateTimeOffset.UtcNow));
            }

            await chatRoomRedisService.RemoveConnectionAsync(connectionId);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Cleanup chat connection failed. ConnectionId: {ConnectionId}, UserId: {UserId}",
                connectionId,
                userId);
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task JoinRoomAsync(string roomId)
    {
        if (!TryValidateRoomId(roomId, out var normalizedRoomId))
        {
            await NotifyCallerAsync("invalid_room_id", "roomId must match [a-zA-Z0-9:_-] and length between 3 and 64.");
            return;
        }

        var (userId, userName) = ResolveIdentity();
        var joinResult = await chatRoomRedisService.JoinRoomAsync(Context.ConnectionId, normalizedRoomId, userId);
        if (!joinResult.Joined)
        {
            await NotifyCallerAsync("already_joined", $"Connection already joined room '{normalizedRoomId}'.",
                                    normalizedRoomId);
            var duplicateSnapshot = await chatRoomRedisService.GetRoomSnapshotAsync(normalizedRoomId, RoomHistoryLimit);
            await Clients.Caller.RoomSnapshot(duplicateSnapshot);
            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, normalizedRoomId);

        if (joinResult.UserFirstConnectionInRoom)
        {
            await Clients.Group(normalizedRoomId).UserJoined(
                new RoomUserEventDto(normalizedRoomId, userId, userName, DateTimeOffset.UtcNow));
        }

        var snapshot = await chatRoomRedisService.GetRoomSnapshotAsync(normalizedRoomId, RoomHistoryLimit);
        await Clients.Caller.RoomSnapshot(snapshot);

        logger.LogInformation(
            "User joined room. RoomId: {RoomId}, UserId: {UserId}, ConnectionId: {ConnectionId}",
            normalizedRoomId,
            userId,
            Context.ConnectionId);
    }

    public async Task LeaveRoomAsync(string roomId)
    {
        if (!TryValidateRoomId(roomId, out var normalizedRoomId))
        {
            await NotifyCallerAsync("invalid_room_id", "roomId must match [a-zA-Z0-9:_-] and length between 3 and 64.");
            return;
        }

        var (userId, userName) = ResolveIdentity();
        var leaveResult = await chatRoomRedisService.LeaveRoomAsync(Context.ConnectionId, normalizedRoomId, userId);
        if (!leaveResult.Left)
        {
            await NotifyCallerAsync("not_in_room", $"Connection is not in room '{normalizedRoomId}'.",
                                    normalizedRoomId);
            return;
        }

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, normalizedRoomId);
        if (leaveResult.UserRemovedFromRoom)
        {
            await Clients.Group(normalizedRoomId).UserLeft(
                new RoomUserEventDto(normalizedRoomId, userId, userName, DateTimeOffset.UtcNow));
        }

        await NotifyCallerAsync("left_room", $"Left room '{normalizedRoomId}'.", normalizedRoomId);
        logger.LogInformation(
            "User left room. RoomId: {RoomId}, UserId: {UserId}, ConnectionId: {ConnectionId}",
            normalizedRoomId,
            userId,
            Context.ConnectionId);
    }

    public async Task SendRoomMessageAsync(string roomId, string content)
    {
        if (!TryValidateRoomId(roomId, out var normalizedRoomId))
        {
            await NotifyCallerAsync("invalid_room_id", "roomId must match [a-zA-Z0-9:_-] and length between 3 and 64.");
            return;
        }

        var normalizedContent = content?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(normalizedContent) || normalizedContent.Length > MaxMessageLength)
        {
            await NotifyCallerAsync("invalid_message_content", "content must be between 1 and 500 characters.");
            return;
        }

        var joined = await chatRoomRedisService.IsConnectionInRoomAsync(Context.ConnectionId, normalizedRoomId);
        if (!joined)
        {
            await NotifyCallerAsync("must_join_room", $"Join room '{normalizedRoomId}' before sending messages.",
                                    normalizedRoomId);
            return;
        }

        var (userId, userName) = ResolveIdentity();
        var message = new ChatMessageDto(
            Guid.NewGuid().ToString("N"),
            normalizedRoomId,
            userId,
            userName,
            normalizedContent,
            DateTimeOffset.UtcNow);

        await chatRoomRedisService.AppendRoomMessageAsync(message, RoomHistoryLimit);
        await Clients.Group(normalizedRoomId).RoomMessageReceived(message);

        logger.LogInformation(
            "Room message broadcasted. RoomId: {RoomId}, UserId: {UserId}, MessageId: {MessageId}",
            normalizedRoomId,
            userId,
            message.MessageId);
    }

    public async Task SyncRoomStateAsync(string roomId)
    {
        if (!TryValidateRoomId(roomId, out var normalizedRoomId))
        {
            await NotifyCallerAsync("invalid_room_id", "roomId must match [a-zA-Z0-9:_-] and length between 3 and 64.");
            return;
        }

        var joined = await chatRoomRedisService.IsConnectionInRoomAsync(Context.ConnectionId, normalizedRoomId);
        if (!joined)
        {
            await NotifyCallerAsync("must_join_room", $"Join room '{normalizedRoomId}' before syncing room state.",
                                    normalizedRoomId);
            return;
        }

        var snapshot = await chatRoomRedisService.GetRoomSnapshotAsync(normalizedRoomId, RoomHistoryLimit);
        await Clients.Caller.RoomSnapshot(snapshot);
    }

    private async Task NotifyCallerAsync(string code, string message, string? roomId = null)
    {
        await Clients.Caller.SystemNotice(new SystemNoticeDto(code, message, roomId));
    }

    private static bool TryValidateRoomId(string roomId, out string normalizedRoomId)
    {
        normalizedRoomId = roomId?.Trim() ?? string.Empty;
        return RoomIdPattern.IsMatch(normalizedRoomId);
    }

    private (string UserId, string UserName) ResolveIdentity()
    {
        var principal = Context.User;
        var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? principal?.FindFirst("sub")?.Value
                     ?? principal?.Identity?.Name
                     ?? Context.ConnectionId;
        var userName = principal?.Identity?.Name ?? userId;
        return ( userId, userName );
    }
}
