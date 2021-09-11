using System;

namespace TileGameServer.Infrastructure.Notifications
{
    public class JoinGameNotification
    {
        public Guid PlayerId { get; set; }
        public Guid GameSessionId { get; set; }
        public string PlayerNickname { get; set; }
    }
}