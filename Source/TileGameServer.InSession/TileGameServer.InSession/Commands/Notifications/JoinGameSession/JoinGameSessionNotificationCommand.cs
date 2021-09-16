using MediatR;
using TileGameServer.BaseLibrary.Domain.MessageQueueNotifications;

namespace TileGameServer.InSession.Commands.Notifications.JoinGameSession
{
    public class JoinGameSessionNotificationCommand : JoinGameSessionNotification, IRequest
    {
    }
}