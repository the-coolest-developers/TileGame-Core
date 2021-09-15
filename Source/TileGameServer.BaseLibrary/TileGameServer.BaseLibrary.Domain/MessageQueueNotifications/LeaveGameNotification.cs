using System;

namespace TileGameServer.BaseLibrary.Domain.MessageQueueNotifications
{
    public class LeaveGameNotification
    {
        public Guid PlayerId { get; set; }
    }
}