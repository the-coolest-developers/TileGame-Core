using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TileGameServer.InSession.DataAccess.Context;
using TileGameServer.InSession.DataAccess.Entities;

namespace TileGameServer.InSession.Commands.Notifications.JoinGameSession
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
                    Nickname = request.PlayerNickname
                };

                session.Players.Add(sessionPlayer);
            }

            return Unit.Task;
        }
    }
}