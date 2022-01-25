using MediatR;
using TileGameServer.InSession.Notifications.CreateGameSession;
using TileGameServer.InSession.Notifications.JoinGameSession;
using TileGameServer.InSession.Notifications.LeaveGameSession;
using WebApiBaseLibrary.MessageQueueing.Attributes;

namespace TileGameServer.InSession.MessageQueueServices
{
    [MessageQueueService]
    public class MenuMessageQueueService
    {
        private readonly IMediator _mediator;

        public MenuMessageQueueService(IMediator mediator)
        {
            _mediator = mediator;
        }

        [MessageQueueAction("CreateGameQueue")]
        public void ReceiveJoinGameNotification(CreateGameSessionNotificationCommand command)
        {
            _mediator.Send(command);
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