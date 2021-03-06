using System;
using MediatR;

namespace TileGameServer.InSession.Notifications.JoinGameSession
{
    public class JoinGameSessionNotificationCommand : IRequest
    {
        public Guid PlayerId { get; set; }
        public Guid GameSessionId { get; set; }
        public string PlayerNickname { get; set; }
    }
}