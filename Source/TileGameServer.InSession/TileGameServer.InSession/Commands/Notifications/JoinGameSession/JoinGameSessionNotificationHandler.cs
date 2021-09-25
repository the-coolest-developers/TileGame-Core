using MediatR;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TileGameServer.InSession.Commands.Notifications.JoinGameSession
{
    public class JoinGameSessionNotificationHandler : IRequestHandler<JoinGameSessionNotificationCommand>
    {
        public Task<Unit> Handle(JoinGameSessionNotificationCommand request, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"A player has joined the game: {request.PlayerNickname}");

            return Unit.Task;
        }
    }
}