using System;
using MediatR;
using WebApiBaseLibrary.Enums;

namespace TileGameServer.Commands.Menu.Notifications.CreateGameSession
{
    public class CreateGameSessionNotificationCommand : IRequest
    {
        public ResponseStatus ResponseStatus { get; set; }

        public Guid GameSessionId { get; set; }
    }
}