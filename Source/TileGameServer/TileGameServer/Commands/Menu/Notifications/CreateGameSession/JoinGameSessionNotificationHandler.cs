using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.BaseLibrary.Domain.MessageQueueNotifications;
using WebApiBaseLibrary.Enums;
using WebApiBaseLibrary.Infrastructure.MessageQueueing;

namespace TileGameServer.Commands.Menu.Notifications.CreateGameSession
{
    public class JoinGameSessionNotificationHandler : IRequestHandler<CreateGameSessionNotificationCommand>
    {
        private readonly IMessageQueuePublisher _messageQueuePublisher;

        public JoinGameSessionNotificationHandler(IMessageQueueConnection messageQueueConnection)
        {
            _messageQueuePublisher = messageQueueConnection.CreatePublisher("CreateGameQueue");
        }

        public Task<Unit> Handle(CreateGameSessionNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.ResponseStatus == ResponseStatus.Success)
            {
                var n = new CreateGameSessionNotification()
                {
                    GameSessionId = request.GameSessionId
                };

                _messageQueuePublisher.PublishMessage(
                    new CreateGameSessionNotification
                    {
                        GameSessionId = request.GameSessionId
                    });

                _messageQueuePublisher.Dispose();
            }

            return Unit.Task;
        }
    }
}