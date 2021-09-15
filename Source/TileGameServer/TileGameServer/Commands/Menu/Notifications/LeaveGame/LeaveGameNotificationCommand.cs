using System;
using MediatR;
using WebApiBaseLibrary.Enums;

namespace TileGameServer.Commands.Menu.Notifications.LeaveGame
{
    public class LeaveGameNotificationCommand : IRequest<Unit>
    {
        public ResponseStatus ResponseStatus { get; set; }
        
        public Guid PlayerId { get; set; }

    }
}