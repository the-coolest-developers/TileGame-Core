using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.InSession.DataAccess.Context;
using TileGameServer.InSession.Domain;
using TileGameServer.InSession.Domain.Creators;
using TileGameServer.InSession.Domain.Entities;

namespace TileGameServer.InSession.Notifications.CreateGameSession
{
    public class CreateGameSessionNotificationHandler : IRequestHandler<CreateGameSessionNotificationCommand>
    {
        private readonly IInSessionContext _inSessionContext;
        private readonly ITileFieldFactory _tileFieldFactory;

        public CreateGameSessionNotificationHandler(
            IInSessionContext inSessionContext,
            ITileFieldFactory tileFieldFactory)
        {
            _inSessionContext = inSessionContext;
            _tileFieldFactory = tileFieldFactory;
        }

        public Task<Unit> Handle(CreateGameSessionNotificationCommand request, CancellationToken cancellationToken)
        {
            var sessions = _inSessionContext.EntitySet<GameSession>();
            if (sessions.Any(s => s.Id == request.GameSessionId)) return Unit.Task;

            var tileFieldSize = new FieldSize
            {
                Width = 100,
                Height = 100
            };
            var tileField = _tileFieldFactory.CreateTileField(tileFieldSize);

            var gameSession = new GameSession(tileField)
            {
                Id = request.GameSessionId,
            };

            sessions.Add(gameSession);

            return Unit.Task;
        }
    }
}