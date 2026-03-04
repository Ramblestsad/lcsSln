using Todo.WebApi.Services.Realtime;

namespace Todo.WebApi.Hubs;

public interface IChatClient
{
    Task RoomSnapshot(RoomSnapshotDto snapshot);

    Task RoomMessageReceived(ChatMessageDto message);

    Task UserJoined(RoomUserEventDto evt);

    Task UserLeft(RoomUserEventDto evt);

    Task SystemNotice(SystemNoticeDto notice);
}
