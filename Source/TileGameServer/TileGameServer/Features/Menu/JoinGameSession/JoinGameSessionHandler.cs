using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.BaseLibrary.Domain.Enums;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.Repositories.GameSessions;
using WebApiBaseLibrary.Authorization.Constants;
using WebApiBaseLibrary.Authorization.Generators;
using WebApiBaseLibrary.Enums;
using WebApiBaseLibrary.Extensions;
using WebApiBaseLibrary.Responses;
using ClaimTypes = TileGameServer.Constants.ClaimTypes;

namespace TileGameServer.Features.Menu.JoinGameSession
{
    public class
        JoinGameSessionCommandHandler : IRequestHandler<JoinGameSessionCommand, IResponse<Unit>>
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

        public async Task<IResponse<Unit>> Handle(
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
                    return new Response<Unit>
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

                    return new Response<Unit>
                    {
                        Status = ResponseStatus.Success
                    };
                }
            }

            return new Response<Unit>
            {
                Status = ResponseStatus.Conflict
            };
        }
    }
}