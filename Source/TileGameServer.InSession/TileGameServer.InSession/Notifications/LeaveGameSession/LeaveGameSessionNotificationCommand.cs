using System;
using MediatR;

namespace TileGameServer.InSession.Notifications.LeaveGameSession
{
    public class LeaveGameSessionNotificationCommand : IRequest
    {
        public Guid PlayerId { get; set; }
    }
}