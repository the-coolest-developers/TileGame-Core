using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.InSession.DataAccess.Context;
using TileGameServer.InSession.Domain.Entities;

namespace TileGameServer.InSession.Notifications.LeaveGameSession
{
    public class LeaveGameSessionNotificationHandler : IRequestHandler<LeaveGameSessionNotificationCommand>
    {
        private readonly IInSessionContext _inSessionContext;

        public LeaveGameSessionNotificationHandler(IInSessionContext inSessionContext)
        {
            _inSessionContext = inSessionContext;
        }

        public Task<Unit> Handle(LeaveGameSessionNotificationCommand request, CancellationToken cancellationToken)
        {
            var sessions = _inSessionContext.EntitySet<GameSession>();
            var sessionWithPlayer = sessions.FirstOrDefault(s => s.Players.Any(p => p.Id == request.PlayerId));

            if (sessionWithPlayer != null)
            {
                var sessionPlayer = sessionWithPlayer.Players.Single(p => p.Id == request.PlayerId);

                sessionWithPlayer.Players.Remove(sessionPlayer);

                if (!sessionWithPlayer.Players.Any())
                {
                    sessions.Remove(sessionWithPlayer);
                }
            }

            return Unit.Task;
        }
    }
}