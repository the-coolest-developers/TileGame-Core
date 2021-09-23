using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using TileGameServer.InSession.Attributes;
using TileGameServer.InSession.Commands.Notifications.JoinGameSession;
using TileGameServer.InSession.Commands.Notifications.LeaveGameSession;

namespace TileGameServer.InSession.HostedServices
{
    public class MessageQueueService
    {
        private readonly IMediator _mediator;

        public MessageQueueService(IMediator mediator)
        {
            _mediator = mediator;
        }

        [QueueAction("JoinGameQueue")]
        public void JoinGameNotificationHandler(JoinGameSessionNotificationCommand command)
        {
            _mediator.Send(command);
        }

        [QueueAction("LeaveGameQueue")]
        public void LeaveGameNotificationHandler(LeaveGameSessionNotificationCommand command)
        {
            _mediator.Send(command);
        }
    }
}