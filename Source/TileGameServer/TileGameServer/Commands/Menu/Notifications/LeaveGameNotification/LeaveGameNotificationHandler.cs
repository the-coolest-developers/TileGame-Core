using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.BaseLibrary.DataAccess.Repositories;
using TileGameServer.Commands.Menu.Notifications.JoinGameNotification;
using TileGameServer.Infrastructure.MessageQueueing;
using WebApiBaseLibrary.Enums;

namespace TileGameServer.Commands.Menu.Notifications.LeaveGameNotification
{
    public class LeaveGameNotificationHandler : IRequestHandler<JoinGameNotificationCommand>
    {
        private readonly IMessageQueuePublisher _leaveGamePublisher;
        private readonly IPlayerRepository _playerRepository;

        public LeaveGameNotificationHandler(
            IMessageQueueConnection messageQueueConnection,
            IPlayerRepository playerRepository)
        {
            _leaveGamePublisher = messageQueueConnection.CreatePublisher("LeaveGameQueue");
            _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(JoinGameNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.ResponseStatus == ResponseStatus.Success)
            {
                var playerExists = await _playerRepository.ExistsWithIdAsync(request.PlayerId);
                if (playerExists)
                {
                    _leaveGamePublisher.PublishMessage(
                        new Infrastructure.Notifications.LeaveGameNotification
                        {
                            PlayerId = request.PlayerId
                        });
                    _leaveGamePublisher.Dispose();
                }
            }

            return Unit.Value;
        }
    }
}