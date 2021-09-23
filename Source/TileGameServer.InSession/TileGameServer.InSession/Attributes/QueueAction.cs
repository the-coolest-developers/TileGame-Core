using System;

namespace TileGameServer.InSession.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class QueueAction : Attribute
    {
        private readonly string _queueName;

        public QueueAction(string queueName)
        {
            _queueName = queueName;
        }
    }
}