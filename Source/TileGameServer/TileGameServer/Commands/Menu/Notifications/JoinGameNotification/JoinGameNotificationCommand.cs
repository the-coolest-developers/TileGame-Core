using System;
using MediatR;
using WebApiBaseLibrary.Enums;

namespace TileGameServer.Commands.Menu.Notifications.JoinGameNotification
{
    public class JoinGameNotificationCommand : IRequest<Unit>
    {
        public ResponseStatus ResponseStatus { get; set; }
        
        public Guid PlayerId { get; set; }
        public Guid GameSessionId { get; set; }

        public string PlayerNickname { get; set; }
    }
}