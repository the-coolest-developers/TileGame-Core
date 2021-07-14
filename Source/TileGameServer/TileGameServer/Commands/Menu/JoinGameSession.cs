using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;
using TileGameServer.DataAccess.Repositories;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.Enums;
using TileGameServer.Extensions;
using TileGameServer.Infrastructure.Generators;

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
            private readonly IJwtGenerator _jwtGenerator;

            public JoinGameSessionCommandHandler(IGameSessionRepository gameSessionsRepository,
                IJwtGenerator jwtGenerator)
            {
                _gameSessionsRepository = gameSessionsRepository;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<JoinGameSessionResponse> Handle(
                JoinGameSessionCommand request,
                CancellationToken cancellationToken)
            {
                var playerIsInSession = await _gameSessionsRepository.ExistsWithPlayerAsync(request.UserId);
                if (!playerIsInSession)
                {
                    GameSession session = await _gameSessionsRepository.GetAsync(request.SessionId);
                    if (session.Status == GameSessionStatus.Created)
                    {
                        session.PlayerIds.Add(request.UserId);

                        var token = _jwtGenerator.GenerateToken(
                            new[]
                            {
                                new Claim(ApplicationClaimTypes.UserId, request.UserId.ToString()),
                                new Claim(ApplicationClaimTypes.SessionId, session.Id.ToString())
                            });

                        return new JoinGameSessionResponse
                        {
                            Status = ResponseStatus.Success,
                            Result = token
                        };
                    }
                }

                return new JoinGameSessionResponse
                {
                    Status = ResponseStatus.Conflict
                };
            }
        }

        public class JoinGameSessionResponse : IResponse<string>
        {
            public string Result { get; set; }
            public ResponseStatus Status { get; set; }
        }
    }
}