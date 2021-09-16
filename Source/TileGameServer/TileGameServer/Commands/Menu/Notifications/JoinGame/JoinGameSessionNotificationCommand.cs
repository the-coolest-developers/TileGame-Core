using System;
using MediatR;
using WebApiBaseLibrary.Enums;

namespace TileGameServer.Commands.Menu.Notifications.JoinGame
{
    public class JoinGameSessionNotificationCommand : IRequest<Unit>
    {
        public ResponseStatus ResponseStatus { get; set; }
        
        public Guid PlayerId { get; set; }
        public Guid GameSessionId { get; set; }
    }
}