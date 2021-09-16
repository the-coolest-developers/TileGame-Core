using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.BaseLibrary.DataAccess.Repositories;
using TileGameServer.BaseLibrary.Domain.MessageQueueNotifications;
using WebApiBaseLibrary.Enums;
using WebApiBaseLibrary.Infrastructure.MessageQueueing;

namespace TileGameServer.Commands.Menu.Notifications.JoinGame
{
    public class JoinGameSessionNotificationHandler : IRequestHandler<JoinGameSessionNotificationCommand>
    {
        private readonly IMessageQueuePublisher _joinGamePublisher;
        private readonly IPlayerRepository _playerRepository;

        public JoinGameSessionNotificationHandler(
            IMessageQueueConnection messageQueueConnection,
            IPlayerRepository playerRepository)
        {
            _joinGamePublisher = messageQueueConnection.CreatePublisher("JoinGameQueue");
            _playerRepository = playerRepository;
        }

        public Task<Unit> Handle(JoinGameSessionNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.ResponseStatus == ResponseStatus.Success)
            {
                var player = _playerRepository.Get(request.PlayerId);
                if (player != null)
                {
                    _joinGamePublisher.PublishMessage(
                        new JoinGameNotification
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