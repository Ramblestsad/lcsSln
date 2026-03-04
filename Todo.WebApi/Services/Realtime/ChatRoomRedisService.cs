using System.Text.Json;
using StackExchange.Redis;

namespace Todo.WebApi.Services.Realtime;

public class ChatRoomRedisService: IChatRoomRedisService
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    private readonly IDatabase redisDatabase;
    private readonly ILogger<ChatRoomRedisService> logger;

    public ChatRoomRedisService(
        IConnectionMultiplexer connectionMultiplexer,
        ILogger<ChatRoomRedisService> logger)
    {
        redisDatabase = connectionMultiplexer.GetDatabase();
        this.logger = logger;
    }

    public async Task<RoomJoinResult> JoinRoomAsync(
        string connectionId,
        string roomId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var connectionRoomsKey = GetConnectionRoomsKey(connectionId);
            var joined = await redisDatabase.SetAddAsync(connectionRoomsKey, roomId);
            if (!joined)
            {
                return new RoomJoinResult(false, false);
            }

            var roomUserConnectionCountKey = GetRoomUserConnectionCountKey(roomId, userId);
            var userConnectionCount = await redisDatabase.StringIncrementAsync(roomUserConnectionCountKey);
            var userFirstConnectionInRoom = userConnectionCount == 1;
            if (userFirstConnectionInRoom)
            {
                await redisDatabase.SetAddAsync(GetRoomMembersKey(roomId), userId);
            }

            logger.LogInformation(
                "Join room success. RoomId: {RoomId}, UserId: {UserId}, ConnectionId: {ConnectionId}, UserConnectionCount: {UserConnectionCount}",
                roomId,
                userId,
                connectionId,
                userConnectionCount);

            return new RoomJoinResult(true, userFirstConnectionInRoom);
        }
        catch (RedisException ex)
        {
            logger.LogError(
                ex,
                "Join room failed. RoomId: {RoomId}, UserId: {UserId}, ConnectionId: {ConnectionId}",
                roomId,
                userId,
                connectionId);
            throw;
        }
    }

    public async Task<RoomLeaveResult> LeaveRoomAsync(
        string connectionId,
        string roomId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var connectionRoomsKey = GetConnectionRoomsKey(connectionId);
            var left = await redisDatabase.SetRemoveAsync(connectionRoomsKey, roomId);
            if (!left)
            {
                return new RoomLeaveResult(false, false);
            }

            var roomUserConnectionCountKey = GetRoomUserConnectionCountKey(roomId, userId);
            var userConnectionCount = await redisDatabase.StringDecrementAsync(roomUserConnectionCountKey);

            var userRemovedFromRoom = false;
            if (userConnectionCount <= 0)
            {
                await redisDatabase.KeyDeleteAsync(roomUserConnectionCountKey);
                await redisDatabase.SetRemoveAsync(GetRoomMembersKey(roomId), userId);
                userRemovedFromRoom = true;
            }

            logger.LogInformation(
                "Leave room success. RoomId: {RoomId}, UserId: {UserId}, ConnectionId: {ConnectionId}, RemainingUserConnectionCount: {UserConnectionCount}",
                roomId,
                userId,
                connectionId,
                userConnectionCount);

            return new RoomLeaveResult(true, userRemovedFromRoom);
        }
        catch (RedisException ex)
        {
            logger.LogError(
                ex,
                "Leave room failed. RoomId: {RoomId}, UserId: {UserId}, ConnectionId: {ConnectionId}",
                roomId,
                userId,
                connectionId);
            throw;
        }
    }

    public async Task<bool> IsConnectionInRoomAsync(
        string connectionId,
        string roomId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await redisDatabase.SetContainsAsync(GetConnectionRoomsKey(connectionId), roomId);
        }
        catch (RedisException ex)
        {
            logger.LogError(
                ex,
                "Check connection room membership failed. RoomId: {RoomId}, ConnectionId: {ConnectionId}",
                roomId,
                connectionId);
            throw;
        }
    }

    public async Task<IReadOnlyList<string>> GetConnectionRoomsAsync(
        string connectionId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var values = await redisDatabase.SetMembersAsync(GetConnectionRoomsKey(connectionId));
            return values
                .Select(value => value.ToString())
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Distinct(StringComparer.Ordinal)
                .ToArray();
        }
        catch (RedisException ex)
        {
            logger.LogError(
                ex,
                "Get connection rooms failed. ConnectionId: {ConnectionId}",
                connectionId);
            throw;
        }
    }

    public async Task RemoveConnectionAsync(string connectionId, CancellationToken cancellationToken = default)
    {
        try
        {
            await redisDatabase.KeyDeleteAsync(GetConnectionRoomsKey(connectionId));
        }
        catch (RedisException ex)
        {
            logger.LogError(ex, "Remove connection room mapping failed. ConnectionId: {ConnectionId}", connectionId);
            throw;
        }
    }

    public async Task AppendRoomMessageAsync(
        ChatMessageDto message,
        int historyLimit = 50,
        CancellationToken cancellationToken = default)
    {
        if (historyLimit <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(historyLimit), "historyLimit must be greater than zero.");
        }

        try
        {
            var payload = JsonSerializer.Serialize(message, SerializerOptions);
            var roomHistoryKey = GetRoomHistoryKey(message.RoomId);
            await redisDatabase.ListLeftPushAsync(roomHistoryKey, payload);
            await redisDatabase.ListTrimAsync(roomHistoryKey, 0, historyLimit - 1);
        }
        catch (RedisException ex)
        {
            logger.LogError(
                ex,
                "Append room message failed. RoomId: {RoomId}, UserId: {UserId}, MessageId: {MessageId}",
                message.RoomId,
                message.UserId,
                message.MessageId);
            throw;
        }
    }

    public async Task<RoomSnapshotDto> GetRoomSnapshotAsync(
        string roomId,
        int historyLimit = 50,
        CancellationToken cancellationToken = default)
    {
        if (historyLimit <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(historyLimit), "historyLimit must be greater than zero.");
        }

        try
        {
            var membersTask = redisDatabase.SetMembersAsync(GetRoomMembersKey(roomId));
            var messagesTask = redisDatabase.ListRangeAsync(GetRoomHistoryKey(roomId), 0, historyLimit - 1);
            await Task.WhenAll(membersTask, messagesTask);

            var members = membersTask.Result
                .Select(value => value.ToString())
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .OrderBy(value => value, StringComparer.Ordinal)
                .ToArray();

            var recentMessages = new List<ChatMessageDto>();
            foreach (var payload in messagesTask.Result)
            {
                if (payload.IsNullOrEmpty)
                {
                    continue;
                }

                try
                {
                    var payloadText = payload.ToString();
                    if (string.IsNullOrWhiteSpace(payloadText))
                    {
                        continue;
                    }

                    var message = JsonSerializer.Deserialize<ChatMessageDto>(payloadText, SerializerOptions);
                    if (message is not null)
                    {
                        recentMessages.Add(message);
                    }
                }
                catch (JsonException ex)
                {
                    logger.LogWarning(
                        ex,
                        "Ignore invalid room message payload. RoomId: {RoomId}, Payload: {Payload}",
                        roomId,
                        payload.ToString());
                }
            }

            recentMessages.Reverse();
            return new RoomSnapshotDto(
                roomId,
                members,
                recentMessages,
                DateTimeOffset.UtcNow);
        }
        catch (RedisException ex)
        {
            logger.LogError(ex, "Get room snapshot failed. RoomId: {RoomId}", roomId);
            throw;
        }
    }

    private static string GetRoomMembersKey(string roomId) => $"chat:room:{roomId}:members";

    private static string GetRoomHistoryKey(string roomId) => $"chat:room:{roomId}:history";

    private static string GetConnectionRoomsKey(string connectionId) => $"chat:conn:{connectionId}:rooms";

    private static string GetRoomUserConnectionCountKey(string roomId, string userId) =>
        $"chat:room:{roomId}:user:{userId}:connCount";
}
