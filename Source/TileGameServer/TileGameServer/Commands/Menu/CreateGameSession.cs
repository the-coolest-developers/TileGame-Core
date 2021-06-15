using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.DataAccess.Repositories;
using TileGameServer.Hubs;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;
using TileGameServer.Infrastructure.Models.Entities;

namespace TileGameServer.Commands.Menu
{
    public class CreateGameSession
    {
        public class CreateGameSessionCommand : IRequest<CreateGameSessionResponse>
        {
            public Guid UserId { get; set; }
        }

        public class CreateGameSessionCommandHandler : IRequestHandler<CreateGameSessionCommand, CreateGameSessionResponse>
        {
            private IGameSessionRepository GameSessionRepository { get; }

            public CreateGameSessionCommandHandler(IGameSessionRepository gameSessionRepository)
            {
                GameSessionRepository = gameSessionRepository;
            }

            public async Task<CreateGameSessionResponse> Handle(CreateGameSessionCommand request, CancellationToken cancellationToken)
            {
                var session = new GameSession()
                {
                    Id = Guid.NewGuid(),
                    Status = GameSessionStatus.Closed,
                    CreationDate = DateTime.Now
                };

                await GameSessionRepository.CreateAsync(session);
                await GameSessionRepository.SaveChangesAsync();

                return new CreateGameSessionResponse
                {
                    Status = ResponseStatus.Success
                };
            }
        }

        public class CreateGameSessionResponse : IResponse<Unit>
        {
            public Unit Result { get; }
            public ResponseStatus Status { get; set; }
        }
    }
}