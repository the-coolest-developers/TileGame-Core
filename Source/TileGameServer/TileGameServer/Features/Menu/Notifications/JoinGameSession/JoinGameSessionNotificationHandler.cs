using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.DataAccess.Repositories.Players;
using WebApiBaseLibrary.Enums;
using WebApiBaseLibrary.MessageQueueing.MessageQueueing;

namespace TileGameServer.Features.Menu.Notifications.JoinGameSession
{
    public class JoinGameSessionNotificationHandler : IRequestHandler<JoinGameSessionNotificationCommand>
    {
        private readonly IMessageQueuePublisher _messageQueuePublisher;
        private readonly IPlayerRepository _playerRepository;

        public JoinGameSessionNotificationHandler(
            IMessageQueueConnection messageQueueConnection,
            IPlayerRepository playerRepository)
        {
            _messageQueuePublisher = messageQueueConnection.CreatePublisher("JoinGameQueue");
            _playerRepository = playerRepository;
        }

        public Task<Unit> Handle(JoinGameSessionNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.ResponseStatus == ResponseStatus.Success)
            {
                var player = _playerRepository.Get(request.PlayerId);
                if (player != null)
                {
                    _messageQueuePublisher.PublishMessage(
                        new JoinGameSessionNotification
                        {
                            PlayerId = request.PlayerId,
                            PlayerNickname = player.Nickname,
                            GameSessionId = request.GameSessionId
                        });
                    _messageQueuePublisher.Dispose();
                }
            }

            return Unit.Task;
        }
    }

    public class JoinGameSessionNotification
    {
        public Guid PlayerId { get; set; }
        public Guid GameSessionId { get; set; }
        public string PlayerNickname { get; set; }
    }
}