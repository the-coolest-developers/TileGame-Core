using System;

namespace TileGameServer.InSession.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class QueueActionAttribute : Attribute
    {
        public string QueueName { get; }

        public QueueActionAttribute(string queueName)
        {
            QueueName = queueName;
        }
    }
}