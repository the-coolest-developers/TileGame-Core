using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;
using TileGameServer.DataAccess.Repositories;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.Enums;
using TileGameServer.Extensions;
using TileGameServer.Infrastructure.Configurators;
using TileGameServer.Infrastructure.Generators;
using TileGameServer.Infrastructure.Models.Configurations;

namespace TileGameServer.Commands.Menu
{
    public class JoinGameSession
    {
        public class JoinGameSessionCommand : IRequest<Response<JoinGameSessionResponse>>
        {
            public Guid AccountId { get; set; }
            public Guid SessionId { get; set; }
        }

        public class JoinGameSessionCommandHandler
            : IRequestHandler<JoinGameSessionCommand, Response<JoinGameSessionResponse>>
        {
            private readonly IGameSessionRepository _gameSessionsRepository;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly SessionCapacityConfiguration _sessionCapacityConfiguration;

            public JoinGameSessionCommandHandler(IGameSessionRepository gameSessionsRepository,
                IJwtGenerator jwtGenerator, IOptions<SessionCapacityConfiguration> capacityConfiguration)
            {
                _gameSessionsRepository = gameSessionsRepository;
                _jwtGenerator = jwtGenerator;
                _sessionCapacityConfiguration = capacityConfiguration.Value;
            }

            public async Task<Response<JoinGameSessionResponse>> Handle(
                JoinGameSessionCommand request,
                CancellationToken cancellationToken)
            {
                var playerIsInSession = await _gameSessionsRepository.ExistsWithPlayerAsync(request.AccountId);
                if (!playerIsInSession)
                {
                    GameSession session = await _gameSessionsRepository.GetAsync(request.SessionId);
                    bool sessionIsFull = session.PlayerIds.Count > session.Capacity
                                         || session.PlayerIds.Count < _sessionCapacityConfiguration.MinSessionCapacity;
                        
                    if (session.Status == GameSessionStatus.Created && !sessionIsFull)
                    {
                        session.PlayerIds.Add(request.AccountId);

                        var token = _jwtGenerator.GenerateToken(
                            new[]
                            {
                                new Claim(ApplicationClaimTypes.AccountId, request.AccountId.ToString()),
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
            public string Token { get; set; }
        }

        public class JoinGameSessionRequest
        {
            public Guid SessionId { get; set; }
        }
    }
}