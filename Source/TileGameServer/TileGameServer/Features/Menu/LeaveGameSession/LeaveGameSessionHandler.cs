using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.BaseLibrary.Domain.Enums;
using TileGameServer.DataAccess.Repositories.GameSessions;
using TileGameServer.Domain.Configurators.SessionCapacityConfigurators;
using WebApiBaseLibrary.Enums;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Features.Menu.LeaveGameSession
{
    public class LeaveGameSessionCommandHandler
             : IRequestHandler<LeaveGameSessionCommand, Response<Unit>>
    {
        private readonly IGameSessionRepository _gameSessionsRepository;
        private readonly ISessionCapacityConfigurator _sessionCapacityConfigurator;

        public LeaveGameSessionCommandHandler(
            IGameSessionRepository gameSessionsRepository,
            ISessionCapacityConfigurator capacityConfigurator)
        {
            _gameSessionsRepository = gameSessionsRepository;
            _sessionCapacityConfigurator = capacityConfigurator;
        }

        public async Task<Response<Unit>> Handle(
            LeaveGameSessionCommand request,
            CancellationToken cancellationToken)
        {
            var session = await _gameSessionsRepository.GetWithPlayerInOpenSessionsAsync(request.AccountId);

            if (session == null)
            {
                return new Response<Unit>
                {
                    Status = ResponseStatus.Conflict
                };
            }

            var player = session.Players.FirstOrDefault(p => p.Id == request.AccountId);
            session.Players.Remove(player);

            if (session.Players.Count < _sessionCapacityConfigurator.Configuration.MinSessionCapacity
                && session.Status == GameSessionStatus.Running)
            {
                session.Status = GameSessionStatus.Closed;
            }

            await _gameSessionsRepository.SaveChangesAsync();

            return new Response<Unit>
            {
                Status = ResponseStatus.Success
            };
        }
    }
}
