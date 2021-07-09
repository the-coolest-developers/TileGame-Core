using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;
using TileGameServer.DataAccess.Repositories;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.Enums;

namespace TileGameServer.Commands.Menu
{
    public class JoinGameSession
    {
        public class JoinGameSessionCommand : IRequest<JoinGameSessionResponse>
        {
            public Guid UserId { get; set; }
            public Guid SessionId { get; set; }
        }

        public class JoinGameSessionCommandHandler : IRequestHandler<JoinGameSessionCommand, JoinGameSessionResponse>
        {
            private readonly IGameSessionRepository _gameSessionsRepository;

            public JoinGameSessionCommandHandler(IGameSessionRepository gameSessionsRepository)
            {
                _gameSessionsRepository = gameSessionsRepository;
            }

            public async Task<JoinGameSessionResponse> Handle(JoinGameSessionCommand request,
                CancellationToken cancellationToken)
            {
                var playerIsInSession = await _gameSessionsRepository.ExistsWithPlayerAsync(request.UserId);
                if (!playerIsInSession)
                {
                    GameSession session = await _gameSessionsRepository.GetAsync(request.SessionId);
                    if (session.Status == GameSessionStatus.Created)
                    {
                        session.PlayerIds.Add(request.UserId);

                        return new JoinGameSessionResponse
                        {
                            Status = ResponseStatus.Success
                        };
                    }
                }

                return new JoinGameSessionResponse
                {
                    Status = ResponseStatus.Conflict
                };
            }
        }

        public class JoinGameSessionResponse : IResponse<Unit>
        {
            public Unit Result { get; }
            public ResponseStatus Status { get; set; }
        }
    }
}