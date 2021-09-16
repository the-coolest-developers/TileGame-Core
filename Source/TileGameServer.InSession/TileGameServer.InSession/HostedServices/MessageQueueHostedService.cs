using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using TileGameServer.InSession.Commands.Notifications.JoinGameSession;
using TileGameServer.InSession.Commands.Notifications.LeaveGameSession;
using WebApiBaseLibrary.Infrastructure.MessageQueueing;

namespace TileGameServer.InSession.HostedServices
{
    public class MessageQueueHostedService : IHostedService
    {
        private readonly IMessageQueueConnection _connection;
        private readonly IMediator _mediator;

        public MessageQueueHostedService(
            IMessageQueueConnectionFactory connectionFactory,
            IMediator mediator)
        {
            _connection = connectionFactory.CreateConnection();

            _mediator = mediator;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var joinGameReader = _connection.CreateReader("JoinGameQueue");
            joinGameReader.SetReceivedAction(JoinGameNotificationHandler);
            joinGameReader.StartReading();

            var leaveGameReader = _connection.CreateReader("LeaveGameQueue");
            leaveGameReader.SetReceivedAction(LeaveGameNotificationHandler);
            leaveGameReader.StartReading();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Dispose();

            return Task.CompletedTask;
        }

        private void JoinGameNotificationHandler(string message)
        {
            var joinGameNotification = JsonConvert.DeserializeObject<JoinGameSessionNotificationCommand>(message);

            _mediator.Send(joinGameNotification);
        }

        private void LeaveGameNotificationHandler(string message)
        {
            var leaveGameNotification = JsonConvert.DeserializeObject<LeaveGameSessionNotificationCommand>(message);

            _mediator.Send(leaveGameNotification);
        }
    }
}