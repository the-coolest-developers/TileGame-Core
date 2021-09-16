using System;

namespace TileGameServer.BaseLibrary.Domain.MessageQueueNotifications
{
    public class LeaveGameSessionNotification
    {
        public Guid PlayerId { get; set; }
    }
}