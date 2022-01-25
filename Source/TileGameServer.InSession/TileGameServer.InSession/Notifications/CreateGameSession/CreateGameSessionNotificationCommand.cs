using System;
using MediatR;

namespace TileGameServer.InSession.Notifications.CreateGameSession
{
    public class CreateGameSessionNotificationCommand : IRequest
    {
        public Guid GameSessionId { get; set; }
    }
}