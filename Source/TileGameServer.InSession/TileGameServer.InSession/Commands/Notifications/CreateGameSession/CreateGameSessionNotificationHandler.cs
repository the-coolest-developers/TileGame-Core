using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TileGameServer.InSession.DataAccess.Context;
using TileGameServer.InSession.DataAccess.Entities;

namespace TileGameServer.InSession.Commands.Notifications.CreateGameSession
{
    public class CreateGameSessionNotificationHandler : IRequestHandler<CreateGameSessionNotificationCommand>
    {
        private readonly IInSessionContext _inSessionContext;

        public CreateGameSessionNotificationHandler(IInSessionContext inSessionContext)
        {
            _inSessionContext = inSessionContext;
        }

        public Task<Unit> Handle(CreateGameSessionNotificationCommand request, CancellationToken cancellationToken)
        {
            var sessions = _inSessionContext.EntitySet<GameSession>();
            if (!sessions.Any(s => s.Id == request.GameSessionId))
            {
                var gameSession = new GameSession()
                {
                    Id = request.GameSessionId
                };

                sessions.Add(gameSession);
            }

            return Unit.Task;
        }
    }
}