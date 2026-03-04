namespace Todo.WebApi.Services.Realtime;

public sealed record ChatMessageDto(
    string MessageId,
    string RoomId,
    string UserId,
    string UserName,
    string Content,
    DateTimeOffset SentAtUtc);

public sealed record RoomSnapshotDto(
    string RoomId,
    IReadOnlyList<string> Members,
    IReadOnlyList<ChatMessageDto> RecentMessages,
    DateTimeOffset SyncedAtUtc);

public sealed record RoomUserEventDto(
    string RoomId,
    string UserId,
    string UserName,
    DateTimeOffset AtUtc);

public sealed record SystemNoticeDto(
    string Code,
    string Message,
    string? RoomId = null);

public sealed record RoomJoinResult(
    bool Joined,
    bool UserFirstConnectionInRoom);

public sealed record RoomLeaveResult(
    bool Left,
    bool UserRemovedFromRoom);
