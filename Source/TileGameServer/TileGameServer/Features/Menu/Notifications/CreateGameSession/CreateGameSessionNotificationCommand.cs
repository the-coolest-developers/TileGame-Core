using System;
using MediatR;
using WebApiBaseLibrary.Enums;

namespace TileGameServer.Features.Menu.Notifications.CreateGameSession
{
    public class CreateGameSessionNotificationCommand : IRequest
    {
        public ResponseStatus ResponseStatus { get; set; }

        public Guid GameSessionId { get; set; }
    }
}