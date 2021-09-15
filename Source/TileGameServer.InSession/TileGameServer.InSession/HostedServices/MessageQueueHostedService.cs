using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using TileGameServer.BaseLibrary.Domain.MessageQueueNotifications;
using WebApiBaseLibrary.Infrastructure.MessageQueueing;

namespace TileGameServer.InSession.HostedServices
{
    public class MessageQueueHostedService : IHostedService
    {
        private readonly IMessageQueueConnection _connection;

        public MessageQueueHostedService(IMessageQueueConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.CreateConnection();
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
            var joinGameNotification = JsonConvert.DeserializeObject<JoinGameNotification>(message);

            Debug.WriteLine($"A player has joined the game: {joinGameNotification.PlayerNickname}");
        }

        private void LeaveGameNotificationHandler(string message)
        {
            var leaveGameNotification = JsonConvert.DeserializeObject<LeaveGameNotification>(message);

            Debug.WriteLine($"A player has left the game: {leaveGameNotification.PlayerId}");
        }
    }
}