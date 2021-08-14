using MediatR;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TileGameServer.BaseLibrary.Domain.Entities;
using TileGameServer.BaseLibrary.Domain.Enums;
using TileGameServer.Constants;
using TileGameServer.DataAccess.Repositories;
using WebApiBaseLibrary.Authorization.Constants;
using WebApiBaseLibrary.Authorization.Generators;
using WebApiBaseLibrary.Enums;
using WebApiBaseLibrary.Extensions;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Commands.Menu.JoinGameSession
{
    public class JoinGameSessionCommandHandler
        : IRequestHandler<JoinGameSessionCommand, Response<JoinGameSessionResponse>>
    {
        private readonly IGameSessionRepository _gameSessionsRepository;
        private readonly IJwtGenerator _jwtGenerator;

        public JoinGameSessionCommandHandler(
            IGameSessionRepository gameSessionsRepository,
            IJwtGenerator jwtGenerator)
        {
            _gameSessionsRepository = gameSessionsRepository;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<Response<JoinGameSessionResponse>> Handle(
            JoinGameSessionCommand request,
            CancellationToken cancellationToken)
        {
            var playerIsInSession =
                await _gameSessionsRepository.ExistsWithPlayerInOpenSessionsAsync(request.AccountId);

            if (!playerIsInSession)
            {
                GameSession session = await _gameSessionsRepository.GetAsync(request.SessionId);

                if (session == null)
                {
                    return new Response<JoinGameSessionResponse>
                    {
                        Status = ResponseStatus.Conflict
                    };
                }

                bool sessionIsFull = session.Players.Count >= session.Capacity;

                if (session.Status == GameSessionStatus.Created && !sessionIsFull)
                {
                    session.Players.Add(
                        new SessionPlayer
                        {
                            Id = request.AccountId,
                            GameSession = session,
                            GameSessionId = session.Id
                        });
                    await _gameSessionsRepository.SaveChangesAsync();

                    var token = _jwtGenerator.GenerateToken(
                        new[]
                        {
                            new Claim(WebApiClaimTypes.AccountId, request.AccountId.ToString()),
                            new Claim(TileGameClaimTypes.SessionId, session.Id.ToString())
                        });

                    var response = new JoinGameSessionResponse
                    {
                        Token = token
                    };

                    return response.Success();
                }
            }

            return new Response<JoinGameSessionResponse>
            {
                Status = ResponseStatus.Conflict
            };
        }
    }
}