using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.Enums;
using TileGameServer.DataAccess.Repositories;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Generators;
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

            public CreateGameSessionCommandHandler(
                IGameSessionRepository gameSessionsRepository)
            {
                _gameSessionsRepository = gameSessionsRepository;
            }

            public async Task<Response<CreateGameSessionResponse>> Handle(
                CreateGameSessionCommand request,
                CancellationToken cancellationToken)
            {
                if (await _gameSessionsRepository.ExistsWithPlayerAsync(request.UserId) || request.SessionCapacity < 2)
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
                    SessionCapacity = request.SessionCapacity
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