using System;

namespace TileGameServer.InSession.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class QueueActionAttribute : Attribute
    {
        private readonly string _queueName;

        public QueueActionAttribute(string queueName)
        {
            _queueName = queueName;
        }
    }
}