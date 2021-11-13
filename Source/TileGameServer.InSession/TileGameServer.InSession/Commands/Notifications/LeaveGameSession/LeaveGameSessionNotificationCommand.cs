using System;
using MediatR;
using TileGameServer.BaseLibrary.Domain.MessageQueueNotifications;

namespace TileGameServer.InSession.Commands.Notifications.LeaveGameSession
{
    public class LeaveGameSessionNotificationCommand : IRequest
    {
        public Guid PlayerId { get; set; }
    }
}