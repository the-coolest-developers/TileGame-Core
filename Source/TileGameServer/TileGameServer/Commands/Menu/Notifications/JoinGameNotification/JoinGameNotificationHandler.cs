using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.Infrastructure.MessageQueueing;
using WebApiBaseLibrary.Enums;

namespace TileGameServer.Commands.Menu.Notifications.JoinGameNotification
{
    public class JoinGameNotificationHandler : IRequestHandler<JoinGameNotificationCommand>
    {
        private readonly IMessageQueuePublisher<Infrastructure.Notifications.JoinGameNotification> _joinGamePublisher;

        public JoinGameNotificationHandler(
            IMessageQueuePublisher<Infrastructure.Notifications.JoinGameNotification> joinGamePublisher)
        {
            _joinGamePublisher = joinGamePublisher;
        }

        public Task<Unit> Handle(JoinGameNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.ResponseStatus == ResponseStatus.Success)
            {
                _joinGamePublisher.PublishMessage(
                    new Infrastructure.Notifications.JoinGameNotification
                    {
                        PlayerId = request.PlayerId,
                        PlayerNickname = request.PlayerNickname,
                        GameSessionId = request.GameSessionId
                    });
                _joinGamePublisher.Dispose();
            }

            return Task.FromResult(Unit.Value);
        }
    }
}