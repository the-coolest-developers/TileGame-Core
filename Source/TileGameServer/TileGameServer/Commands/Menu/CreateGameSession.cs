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
        public class CreateGameSessionCommand : IRequest<CreateGameSessionResponse>
        {
            public Guid UserId { get; set; }
        }

        public class CreateGameSessionCommandHandler :
            IRequestHandler<CreateGameSessionCommand, CreateGameSessionResponse>
        {
            private readonly IGameSessionRepository _gameSessionsRepository;

            public CreateGameSessionCommandHandler(
                IGameSessionRepository gameSessionsRepository,
                IJwtGenerator jwtGenerator)
            {
                _gameSessionsRepository = gameSessionsRepository;
            }

            public async Task<CreateGameSessionResponse> Handle(
                CreateGameSessionCommand request,
                CancellationToken cancellationToken)
            {
                if (await _gameSessionsRepository.ExistsWithPlayerAsync(request.UserId))
                {
                    return new CreateGameSessionResponse
                    {
                        Status = ResponseStatus.Conflict
                    };
                }

                var session = new GameSession
                {
                    Id = Guid.NewGuid(),
                    Status = GameSessionStatus.Created,
                    CreationDate = DateTime.Now
                };

                await _gameSessionsRepository.CreateAsync(session);

                return new CreateGameSessionResponse
                {
                    Status = ResponseStatus.Success,
                    Result = session.Id
                };
            }
        }

        public class CreateGameSessionResponse : IResponse<Guid>
        {
            public Guid Result { get; set; }
            public ResponseStatus Status { get; set; }
        }
    }
}