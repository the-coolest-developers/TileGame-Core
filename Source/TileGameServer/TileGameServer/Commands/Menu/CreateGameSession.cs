using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.Enums;
using TileGameServer.DataAccess.Repositories;
using TileGameServer.Infrastructure.Configurators.SessionCapacityConfigurators;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;

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
                ISessionCapacityConfigurator capacityConfiguration)
            {
                _gameSessionsRepository = gameSessionsRepository;
                _sessionCapacityConfigurator = capacityConfiguration;
            }

            public async Task<Response<CreateGameSessionResponse>> Handle(
                CreateGameSessionCommand request,
                CancellationToken cancellationToken)
            {
                bool capacityIsValid = request.SessionCapacity > _sessionCapacityConfigurator.Configuration.MinSessionCapacity
                                       && request.SessionCapacity < _sessionCapacityConfigurator.Configuration.MaxSessionCapacity;
                
                if (await _gameSessionsRepository.ExistsWithPlayerAsync(request.AccountId) || !capacityIsValid)
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

                return new Response<CreateGameSessionResponse>
                {
                    Result = new CreateGameSessionResponse
                    {
                        SessionId = session.Id
                    },
                    Status = ResponseStatus.Success,
                };
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