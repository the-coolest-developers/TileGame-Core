using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.InSession.DataAccess.Context;
using TileGameServer.InSession.Domain.Entities;

namespace TileGameServer.InSession.Notifications.JoinGameSession
{
    public class JoinGameSessionNotificationHandler : IRequestHandler<JoinGameSessionNotificationCommand>
    {
        private readonly IInSessionContext _inSessionContext;

        public JoinGameSessionNotificationHandler(IInSessionContext inSessionContext)
        {
            _inSessionContext = inSessionContext;
        }

        public Task<Unit> Handle(JoinGameSessionNotificationCommand request, CancellationToken cancellationToken)
        {
            var sessions = _inSessionContext.EntitySet<GameSession>();
            var session = sessions.FirstOrDefault(s => s.Id == request.GameSessionId);

            if (session != null && sessions.All(s => s.Players.All(p => p.Id != request.PlayerId)))
            {
                var sessionPlayer = new SessionPlayer
                {
                    Id = request.PlayerId,
                    Nickname = request.PlayerNickname,

                    GameSession = session
                };

                session.Players.Add(sessionPlayer);

                var players = _inSessionContext.EntitySet<SessionPlayer>();
                if (players.All(s => s.Id != request.PlayerId))
                {
                    players.Add(sessionPlayer);
                }
            }

            return Unit.Task;
        }
    }
}