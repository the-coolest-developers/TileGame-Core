using System;

namespace TileGameServer.InSession.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MessageQueueActionAttribute : Attribute
    {
        public string QueueName { get; }

        public MessageQueueActionAttribute(string queueName)
        {
            QueueName = queueName;
        }
    }
}