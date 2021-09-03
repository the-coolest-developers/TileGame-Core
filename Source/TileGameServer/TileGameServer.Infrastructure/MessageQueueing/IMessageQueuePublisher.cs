using System;

namespace TileGameServer.Infrastructure.MessageQueueing
{
    public interface IMessageQueuePublisher : IDisposable
    {
        public void PublishMessage(string messageBody);
        public void PublishMessage<TBody>(TBody messageBody);
    }
}