using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebApiBaseLibrary.Enums;
using WebApiBaseLibrary.Infrastructure.MessageQueueing;

namespace TileGameServer.Commands.Menu.Notifications.CreateGameSession
{
    public class CreateGameSessionNotificationHandler : IRequestHandler<CreateGameSessionNotificationCommand>
    {
        private readonly IMessageQueuePublisher _messageQueuePublisher;

        public CreateGameSessionNotificationHandler(IMessageQueueConnection messageQueueConnection)
        {
            _messageQueuePublisher = messageQueueConnection.CreatePublisher("CreateGameQueue");
        }

        public Task<Unit> Handle(CreateGameSessionNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.ResponseStatus == ResponseStatus.Success)
            {
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

    public class CreateGameSessionNotification
    {
        public Guid GameSessionId { get; set; }
    }
}