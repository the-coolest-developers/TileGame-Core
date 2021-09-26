using MediatR;
using TileGameServer.InSession.Attributes;
using TileGameServer.InSession.Commands.Notifications.JoinGameSession;
using TileGameServer.InSession.Commands.Notifications.LeaveGameSession;

namespace TileGameServer.InSession.HostedServices
{
    [MessageQueueService]
    public class MenuMessageQueueService
    {
        private readonly IMediator _mediator;

        public MenuMessageQueueService(IMediator mediator)
        {
            _mediator = mediator;
        }

        [MessageQueueAction("JoinGameQueue")]
        public void ReceiveJoinGameNotification(JoinGameSessionNotificationCommand command)
        {
            _mediator.Send(command);
        }

        [MessageQueueAction("LeaveGameQueue")]
        public void ReceiveLeaveGameNotification(LeaveGameSessionNotificationCommand command)
        {
            _mediator.Send(command);
        }
    }
}