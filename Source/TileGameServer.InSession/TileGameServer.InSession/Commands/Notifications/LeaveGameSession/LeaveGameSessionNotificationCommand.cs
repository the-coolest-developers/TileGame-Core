using MediatR;
using TileGameServer.BaseLibrary.Domain.MessageQueueNotifications;

namespace TileGameServer.InSession.Commands.Notifications.LeaveGameSession
{
    public class LeaveGameSessionNotificationCommand : LeaveGameSessionNotification, IRequest
    {
    }
}