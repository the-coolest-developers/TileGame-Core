using System.Diagnostics.CodeAnalysis;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace TileGameServer.Infrastructure.MessageQueueing.RabbitMQ
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RabbitMQPublisher : IMessageQueuePublisher
    {
        private readonly IModel _channel;
        private readonly string _queueName;

        public RabbitMQPublisher(IModel channel, string queueName)
        {
            _channel = channel;
            _queueName = queueName;

            _channel.QueueDeclare(
                queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void PublishMessage<TBody>(TBody messageBody)
        {
            var serializedBody = JsonConvert.SerializeObject(messageBody);

            var body = Encoding.UTF8.GetBytes(serializedBody);

            _channel.BasicPublish(
                exchange: string.Empty,
                routingKey: _queueName,
                basicProperties: null,
                body: body);
        }

        public void Dispose()
        {
            _channel?.Dispose();
        }
    }
}