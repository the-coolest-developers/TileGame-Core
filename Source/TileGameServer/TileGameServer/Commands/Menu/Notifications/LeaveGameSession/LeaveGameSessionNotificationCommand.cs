using System;
using MediatR;
using WebApiBaseLibrary.Enums;

namespace TileGameServer.Commands.Menu.Notifications.LeaveGameSession
{
    public class LeaveGameSessionNotificationCommand : IRequest
    {
        public ResponseStatus ResponseStatus { get; set; }

        public Guid PlayerId { get; set; }
    }
}