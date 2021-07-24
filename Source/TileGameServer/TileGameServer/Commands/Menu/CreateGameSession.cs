using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.Enums;
using TileGameServer.DataAccess.Repositories;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Generators;
using TileGameServer.Infrastructure.Models.Configurations;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;

namespace TileGameServer.Commands.Menu
{
    public class CreateGameSession
    {
        public class CreateGameSessionCommand : IRequest<Response<CreateGameSessionResponse>>
        {
            public Guid UserId { get; set; }
            public int SessionCapacity { get; set; }
        }

        public class CreateGameSessionCommandHandler 
            : IRequestHandler<CreateGameSessionCommand, Response<CreateGameSessionResponse>>
        {
            private readonly IGameSessionRepository _gameSessionsRepository;
            private readonly SessionCapacityConfiguration _sessionCapacityConfiguration;
            
            public CreateGameSessionCommandHandler(IGameSessionRepository gameSessionsRepository, 
                IOptions<SessionCapacityConfiguration> capacityConfiguration)
            {
                _gameSessionsRepository = gameSessionsRepository;
                _sessionCapacityConfiguration = capacityConfiguration.Value;
            }

            public async Task<Response<CreateGameSessionResponse>> Handle(
                CreateGameSessionCommand request,
                CancellationToken cancellationToken)
            {
                if (await _gameSessionsRepository.ExistsWithPlayerAsync(request.UserId) 
                    || request.SessionCapacity < _sessionCapacityConfiguration.MinSessionCapacity
                    || request.SessionCapacity > request.SessionCapacity)
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
                    Status = ResponseStatus.Success,
                };
            }
        }

        public class CreateGameSessionResponse
        {
            public Guid UserId { get; set; }
        }
        public class CreateGameSessionRequest
        {
            public int SessionCapacity { get; set; }
        }
    }
}