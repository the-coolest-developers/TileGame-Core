using MediatR;
using TileGameServer.BaseLibrary.Domain.MessageQueueNotifications;

namespace TileGameServer.InSession.Commands.Notifications.CreateGameSession
{
    public class CreateGameSessionNotificationCommand : CreateGameSessionNotification, IRequest
    {
    }
}