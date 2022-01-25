using System;
using MediatR;
using WebApiBaseLibrary.Enums;

namespace TileGameServer.Features.Menu.Notifications.JoinGameSession
{
    public class JoinGameSessionNotificationCommand : IRequest
    {
        public ResponseStatus ResponseStatus { get; set; }

        public Guid PlayerId { get; set; }
        public Guid GameSessionId { get; set; }
    }
}