namespace TileGameServer.Infrastructure.MessageQueueing
{
    public interface IMessageQueuePublisher
    {
        public void PublishMessage(string messageBody);
    }
}