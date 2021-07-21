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
        public class JoinGameSessionCommand : IRequest<Response<JoinGameSessionResponse>>
        {
            public Guid UserId { get; set; }
            public Guid SessionId { get; set; }
        }

        public class JoinGameSessionCommandHandler 
            : IRequestHandler<JoinGameSessionCommand, Response<JoinGameSessionResponse>>
        {
            private readonly IGameSessionRepository _gameSessionsRepository;
            private readonly IJwtGenerator _jwtGenerator;

            public JoinGameSessionCommandHandler(IGameSessionRepository gameSessionsRepository,
                IJwtGenerator jwtGenerator)
            {
                _gameSessionsRepository = gameSessionsRepository;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<Response<JoinGameSessionResponse>> Handle(
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

                        return new Response<JoinGameSessionResponse>
                        {
                            Status = ResponseStatus.Success,
                            Result = new JoinGameSessionResponse
                            {
                                Token = token
                            }
                        };
                    }
                }

                return new Response<JoinGameSessionResponse>
                {
                    Status = ResponseStatus.Conflict
                };
            }
        }

        public class JoinGameSessionResponse
        {
            public Guid UserId { get; set; }
            public Guid SessionId { get; set; }
            public string Token { get; set; }
        }

        public class JoinGameSessionRequest
        {
            public Guid SessionId { get; set; }
        }
    }
}