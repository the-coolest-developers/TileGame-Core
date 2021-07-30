using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.BaseLibrary.Domain.Enums;
using TileGameServer.DataAccess.Repositories;
using TileGameServer.Domain.Configurators.SessionCapacityConfigurators;
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
            private readonly IGameSessionListRepository _gameSessionsListRepository;
            private readonly ISessionCapacityConfigurator _sessionCapacityConfigurator;

            public LeaveGameSessionCommandHandler(
                IGameSessionListRepository gameSessionsListRepository,
                ISessionCapacityConfigurator capacityConfigurator)
            {
                _gameSessionsListRepository = gameSessionsListRepository;
                _sessionCapacityConfigurator = capacityConfigurator;
            }

            public async Task<Response<Unit>> Handle(
                LeaveGameSessionCommand request,
                CancellationToken cancellationToken)
            {
                if (!await _gameSessionsListRepository.ExistsWithPlayerAsync(request.AccountId))
                {
                    return new Response<Unit>
                    {
                        Status = ResponseStatus.Conflict
                    };
                }

                var session = await _gameSessionsListRepository.GetWithPlayerAsync(request.AccountId);
                session.Players.Remove(request.AccountId);

                if (session.Players.Count < _sessionCapacityConfigurator.Configuration.MinSessionCapacity)
                {
                    await _gameSessionsListRepository.DeleteAsync(session.Id);
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