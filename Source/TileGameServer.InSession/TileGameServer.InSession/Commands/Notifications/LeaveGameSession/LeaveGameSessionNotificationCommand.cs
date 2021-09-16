using System;
using MediatR;

namespace TileGameServer.InSession.Commands.Notifications.LeaveGameSession
{
    public class LeaveGameSessionNotificationCommand : IRequest
    {
        public Guid PlayerId { get; set; }
    }
}