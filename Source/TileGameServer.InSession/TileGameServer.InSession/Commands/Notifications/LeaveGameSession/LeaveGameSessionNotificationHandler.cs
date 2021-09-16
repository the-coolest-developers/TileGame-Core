using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TileGameServer.InSession.Commands.Notifications.LeaveGameSession
{
    public class LeaveGameSessionNotificationHandler : IRequestHandler<LeaveGameSessionNotificationCommand>
    {
        public Task<Unit> Handle(LeaveGameSessionNotificationCommand request, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"A player has left the game: {request.PlayerId}");

            return Unit.Task;
        }
    }
}