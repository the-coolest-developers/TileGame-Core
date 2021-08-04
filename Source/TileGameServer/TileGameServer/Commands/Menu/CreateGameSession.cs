using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.BaseLibrary.Domain.Entities;
using TileGameServer.BaseLibrary.Domain.Enums;
using TileGameServer.DataAccess.Repositories;
using TileGameServer.Domain.Configurators.SessionCapacityConfigurators;
using WebApiBaseLibrary.Enums;
using WebApiBaseLibrary.Extensions;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Commands.Menu
{
    public class CreateGameSession
    {
        public class CreateGameSessionCommand : IRequest<Response<CreateGameSessionResponse>>
        {
            public Guid AccountId { get; set; }
            public int SessionCapacity { get; set; }
        }

        public class CreateGameSessionCommandHandler
            : IRequestHandler<CreateGameSessionCommand, Response<CreateGameSessionResponse>>
        {
            private readonly IGameSessionRepository _gameSessionsRepository;
            private readonly ISessionCapacityConfigurator _sessionCapacityConfigurator;

            public CreateGameSessionCommandHandler(
                IGameSessionRepository gameSessionsRepository,
                ISessionCapacityConfigurator capacityConfigurator)
            {
                _gameSessionsRepository = gameSessionsRepository;
                _sessionCapacityConfigurator = capacityConfigurator;
            }

            public async Task<Response<CreateGameSessionResponse>> Handle(
                CreateGameSessionCommand request,
                CancellationToken cancellationToken)
            {
                bool capacityIsValid =
                    request.SessionCapacity >= _sessionCapacityConfigurator.Configuration.MinSessionCapacity
                    && request.SessionCapacity <= _sessionCapacityConfigurator.Configuration.MaxSessionCapacity;

                if (await _gameSessionsRepository.GetWithPlayerFromAllSessionsAsync(request.AccountId) != null || !capacityIsValid)
                {
                    return new Response<CreateGameSessionResponse>
                    {
                        Status = ResponseStatus.Conflict
                    };
                }

                var session = new GameSession
                {
                    Id = Guid.NewGuid(),
                    Status = GameSessionStatus.Created,
                    CreationDate = DateTime.Now,
                    Capacity = request.SessionCapacity
                };

                await _gameSessionsRepository.CreateAsync(session);
                await _gameSessionsRepository.SaveChangesAsync();

                var createGameSessionResponse = new CreateGameSessionResponse
                {
                    SessionId = session.Id
                };

                return createGameSessionResponse.Success();
            }
        }

        public class CreateGameSessionResponse
        {
            public Guid SessionId { get; set; }
        }

        public class CreateGameSessionRequest
        {
            public int SessionCapacity { get; set; }
        }
    }
}