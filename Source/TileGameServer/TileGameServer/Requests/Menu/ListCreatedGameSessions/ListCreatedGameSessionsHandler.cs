using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.BaseLibrary.DataAccess.Repositories;
using TileGameServer.DataAccess.Repositories.GameSessions;
using TileGameServer.Domain.Models.Configurations;
using WebApiBaseLibrary.Extensions;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Requests.Menu.ListCreatedGameSessions
{
    public class ListCreatedGameSessionsHandler :
        IRequestHandler<ListCreatedGameSessionsRequest, IResponse<ListCreatedGameSessionsResponse>>
    {
        private readonly IGameSessionRepository _gameSessionsRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly RequestLimitConfiguration _requestLimitConfiguration;

        public ListCreatedGameSessionsHandler(
            IGameSessionRepository gameSessionsRepository,
            IPlayerRepository playerRepository,
            RequestLimitConfiguration configuration)
        {
            _gameSessionsRepository = gameSessionsRepository;
            _playerRepository = playerRepository;
            _requestLimitConfiguration = configuration;
        }

        public async Task<IResponse<ListCreatedGameSessionsResponse>> Handle(
            ListCreatedGameSessionsRequest request,
            CancellationToken cancellationToken)
        {
            var minRequestLimit = _requestLimitConfiguration.MinRequestLimit;
            var maxRequestLimit = _requestLimitConfiguration.MaxRequestLimit;

            var limit = request.Limit <= minRequestLimit
                        || request.Limit > maxRequestLimit
                ? _requestLimitConfiguration.Default
                : request.Limit;

            var gameSessions = await _gameSessionsRepository.GetTopAsync(request.Offset, limit);

            var listedGameSessions = gameSessions.Select(
                gameSession =>
                {
                    var playerNickname = _playerRepository.Get(gameSession.CreatorId).Nickname;

                    return new ListedGameSession
                    {
                        Id = gameSession.Id,
                        Capacity = gameSession.Capacity,
                        CreatorNickname = playerNickname,
                        PlayerAmount = gameSession.Players.Count
                    };
                });

            var response = new ListCreatedGameSessionsResponse
            {
                GameSessions = listedGameSessions.ToArray()
            };

            return response.Success();
        }
    }
}