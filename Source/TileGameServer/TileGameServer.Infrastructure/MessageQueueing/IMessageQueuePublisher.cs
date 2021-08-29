using System;

namespace TileGameServer.Infrastructure.MessageQueueing
{
    public interface IMessageQueuePublisher : IDisposable
    {
        public void PublishMessage(string messageBody);
    }

    public interface IMessageQueuePublisher<in TBody> : IDisposable
    {
        public void PublishMessage(TBody messageBody);
    }
}