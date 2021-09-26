using System;

namespace TileGameServer.InSession.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MessageQueueServiceAttribute : Attribute
    {
    }
}