using System;

namespace TileGameServer.Infrastructure.MessageQueueing
{
    public interface IMessageQueuePublisher : IDisposable
    {
        public void PublishMessage<TBody>(TBody messageBody);
    }
}