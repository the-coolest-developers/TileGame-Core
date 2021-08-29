namespace TileGameServer.Infrastructure.MessageQueueing
{
    public interface IMessageQueueConnectionFactory
    {
        public IMessageQueueConnection GetConnection();
    }
}
