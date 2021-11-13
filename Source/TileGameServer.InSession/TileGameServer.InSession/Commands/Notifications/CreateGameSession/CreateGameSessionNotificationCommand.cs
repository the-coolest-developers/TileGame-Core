using System;
using MediatR;
using TileGameServer.BaseLibrary.Domain.MessageQueueNotifications;

namespace TileGameServer.InSession.Commands.Notifications.CreateGameSession
{
    public class CreateGameSessionNotificationCommand : IRequest
    {
        public Guid GameSessionId { get; set; }
    }
}