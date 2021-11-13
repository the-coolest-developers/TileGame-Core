using System;

namespace TileGameServer.BaseLibrary.Domain.MessageQueueNotifications
{
    public class CreateGameSessionNotification
    {
        public Guid GameSessionId { get; set; }
    }
}