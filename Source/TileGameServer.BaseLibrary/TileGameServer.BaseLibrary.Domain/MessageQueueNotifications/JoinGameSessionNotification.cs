using System;

namespace TileGameServer.BaseLibrary.Domain.MessageQueueNotifications
{
    public class JoinGameSessionNotification
    {
        public Guid PlayerId { get; set; }
        public Guid GameSessionId { get; set; }
        public string PlayerNickname { get; set; }
    }
}