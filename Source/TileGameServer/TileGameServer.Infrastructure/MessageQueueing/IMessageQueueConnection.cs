namespace TileGameServer.Infrastructure.MessageQueueing
{
    public interface IMessageQueueConnection
    {
        public IMessageQueuePublisher CreatePublisher(string queueName);
        public IMessageQueuePublisher CreatePublisher<TBody>(string queueName);
    }
}