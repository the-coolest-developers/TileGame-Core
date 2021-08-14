using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.DataAccess.Repositories;
using TileGameServer.Domain.Models.Configurations;
using WebApiBaseLibrary.Extensions;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Requests.Menu.ListCreatedGameSessions
{
    public class ListCreatedGameSessionsHandler :
        IRequestHandler<ListCreatedGameSessionsRequest, IResponse<ListCreatedGameSessionsResponse>>
    {
        private readonly IGameSessionRepository _gameSessionsRepository;
        private readonly RequestLimitConfiguration _requestLimitConfiguration;

        public ListCreatedGameSessionsHandler(
            IGameSessionRepository gameSessionsRepository,
            RequestLimitConfiguration configuration)
        {
            _gameSessionsRepository = gameSessionsRepository;
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
                gs => new ListedGameSession
                {
                    Id = gs.Id,
                    Capacity = gs.Capacity,
                    CreatorNickname = "Creator!",
                    PlayerAmount = gs.Players.Count
                }).ToArray();

            var response = new ListCreatedGameSessionsResponse
            {
                GameSessions = listedGameSessions
            };

            return response.Success();
        }
    }
}