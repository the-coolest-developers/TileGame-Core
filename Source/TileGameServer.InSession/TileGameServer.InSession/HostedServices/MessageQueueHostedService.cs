using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using WebApiBaseLibrary.Infrastructure.MessageQueueing;

namespace TileGameServer.InSession.HostedServices
{
    public class MessageQueueHostedService : IHostedService
    {
        private readonly IMessageQueueReader _joinGameReader;
        private readonly IMessageQueueReader _leaveGameReader;

        private readonly IMessageQueueConnection _connection;

        public MessageQueueHostedService(IMessageQueueConnection connection)
        {
            _connection = connection;

            _joinGameReader = connection.CreateReader("JoinGameQueue");
            _joinGameReader.SetReceivedAction(JoinGameNotificationHandler);

            _leaveGameReader = connection.CreateReader("LeaveGameQueue");
            _leaveGameReader.SetReceivedAction(LeaveGameNotificationHandler);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _joinGameReader.StartReading();
            _leaveGameReader.StartReading();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Dispose();

            return Task.CompletedTask;
        }

        private void JoinGameNotificationHandler(string message)
        {
            Debug.WriteLine(" [x] Received {0}", message);
        }

        private void LeaveGameNotificationHandler(string message)
        {
            Debug.WriteLine(" [x] Received {0}", message);
        }
    }
}