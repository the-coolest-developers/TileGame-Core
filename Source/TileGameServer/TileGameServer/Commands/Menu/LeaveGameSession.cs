using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.BaseLibrary.Domain.Enums;
using TileGameServer.DataAccess.Repositories;
using TileGameServer.Infrastructure.Configurators.SessionCapacityConfigurators;
using WebApiBaseLibrary.Enums;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Commands.Menu
{
    public class LeaveGameSession
    {
        public class LeaveGameSessionCommand : IRequest<Response<Unit>>
        {
            public Guid AccountId { get; set; }
        }

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
                if (!await _gameSessionsRepository.ExistsWithPlayerAsync(request.AccountId))
                {
                    return new Response<Unit>
                    {
                        Status = ResponseStatus.Conflict
                    };
                }

                var session = await _gameSessionsRepository.GetWithPlayerAsync(request.AccountId);
                session.Players.Remove(request.AccountId);

                if (session.Players.Count < _sessionCapacityConfigurator.Configuration.MinSessionCapacity)
                {
                    await _gameSessionsRepository.DeleteAsync(session.Id);
                    session.Status = GameSessionStatus.Closed;
                }

                return new Response<Unit>
                {
                    Status = ResponseStatus.Success
                };
            }
        }
    }
}