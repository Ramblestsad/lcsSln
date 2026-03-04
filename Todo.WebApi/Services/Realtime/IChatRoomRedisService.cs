namespace Todo.WebApi.Services.Realtime;

public interface IChatRoomRedisService
{
    Task<RoomJoinResult> JoinRoomAsync(
        string connectionId,
        string roomId,
        string userId,
        CancellationToken cancellationToken = default);

    Task<RoomLeaveResult> LeaveRoomAsync(
        string connectionId,
        string roomId,
        string userId,
        CancellationToken cancellationToken = default);

    Task<bool> IsConnectionInRoomAsync(
        string connectionId,
        string roomId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<string>> GetConnectionRoomsAsync(
        string connectionId,
        CancellationToken cancellationToken = default);

    Task RemoveConnectionAsync(string connectionId, CancellationToken cancellationToken = default);

    Task AppendRoomMessageAsync(
        ChatMessageDto message,
        int historyLimit = 50,
        CancellationToken cancellationToken = default);

    Task<RoomSnapshotDto> GetRoomSnapshotAsync(
        string roomId,
        int historyLimit = 50,
        CancellationToken cancellationToken = default);
}
