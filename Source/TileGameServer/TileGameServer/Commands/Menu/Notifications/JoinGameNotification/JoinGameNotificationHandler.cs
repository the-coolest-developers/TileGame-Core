using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.BaseLibrary.DataAccess.Repositories;
using TileGameServer.Infrastructure.MessageQueueing;
using WebApiBaseLibrary.Enums;

namespace TileGameServer.Commands.Menu.Notifications.JoinGameNotification
{
    public class JoinGameNotificationHandler : IRequestHandler<JoinGameNotificationCommand>
    {
        private readonly IMessageQueuePublisher _joinGamePublisher;
        private readonly IPlayerRepository _playerRepository;

        public JoinGameNotificationHandler(
            IMessageQueueConnection messageQueueConnection,
            IPlayerRepository playerRepository)
        {
            _joinGamePublisher = messageQueueConnection.CreatePublisher("JoinGameQueue");
            _playerRepository = playerRepository;
        }

        public Task<Unit> Handle(JoinGameNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.ResponseStatus == ResponseStatus.Success)
            {
                var player = _playerRepository.Get(request.PlayerId);
                if (player != null)
                {
                    _joinGamePublisher.PublishMessage(
                        new Infrastructure.Notifications.JoinGameNotification
                        {
                            PlayerId = request.PlayerId,
                            PlayerNickname = player.Nickname,
                            GameSessionId = request.GameSessionId
                        });
                    _joinGamePublisher.Dispose();
                }
            }

            return Task.FromResult(Unit.Value);
        }
    }
}