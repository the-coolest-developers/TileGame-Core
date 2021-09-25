using System;

namespace TileGameServer.InSession.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MessageQueueActionAttribute : Attribute
    {
        public string QueueName { get; }

        public MessageQueueActionAttribute(string queueName)
        {
            QueueName = queueName;
        }
    }
}