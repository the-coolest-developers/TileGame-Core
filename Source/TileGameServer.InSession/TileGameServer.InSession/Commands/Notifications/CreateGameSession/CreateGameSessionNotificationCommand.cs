using System;
using MediatR;

namespace TileGameServer.InSession.Commands.Notifications.CreateGameSession
{
    public class CreateGameSessionNotificationCommand : IRequest
    {
        public Guid GameSessionId { get; set; }
    }
}