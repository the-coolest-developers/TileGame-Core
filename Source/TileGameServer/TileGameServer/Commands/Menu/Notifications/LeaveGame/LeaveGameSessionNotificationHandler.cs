using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.BaseLibrary.DataAccess.Repositories;
using TileGameServer.BaseLibrary.Domain.MessageQueueNotifications;
using WebApiBaseLibrary.Enums;
using WebApiBaseLibrary.Infrastructure.MessageQueueing;

namespace TileGameServer.Commands.Menu.Notifications.LeaveGame
{
    public class LeaveGameSessionNotificationHandler : IRequestHandler<LeaveGameSessionNotificationCommand>
    {
        private readonly IMessageQueuePublisher _leaveGamePublisher;
        private readonly IPlayerRepository _playerRepository;

        public LeaveGameSessionNotificationHandler(
            IMessageQueueConnection messageQueueConnection,
            IPlayerRepository playerRepository)
        {
            _leaveGamePublisher = messageQueueConnection.CreatePublisher("LeaveGameQueue");
            _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(LeaveGameSessionNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.ResponseStatus == ResponseStatus.Success)
            {
                var playerExists = await _playerRepository.ExistsWithIdAsync(request.PlayerId);
                if (playerExists)
                {
                    _leaveGamePublisher.PublishMessage(
                        new LeaveGameSessionNotification
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