using System;

namespace TileGameServer.Infrastructure
{
    public class JoinGameNotification
    {
        public Guid PlayerId { get; set; }
        public Guid GameSessionId { get; set; }
        public string PlayerNickname { get; set; }
    }
}