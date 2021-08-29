namespace TileGameServer.Infrastructure.MessageQueueing
{
    public interface IMessageQueuePublisher
    {
        public void PublishMessage(string messageBody);
    }

    public interface IMessageQueuePublisher<in TBody>
    {
        public void PublishMessage(TBody messageBody);
    }
}