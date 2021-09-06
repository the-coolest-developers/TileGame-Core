using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.BaseLibrary.DataAccess.Repositories;
using WebApiBaseLibrary.Enums;
using WebApiBaseLibrary.Infrastructure.MessageQueueing;

namespace TileGameServer.Commands.Menu.Notifications.LeaveGameNotification
{
    public class LeaveGameNotificationHandler : IRequestHandler<LeaveGameNotificationCommand>
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

        public async Task<Unit> Handle(LeaveGameNotificationCommand request, CancellationToken cancellationToken)
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