using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.DataAccess.Repositories;
using WebApiBaseLibrary.Extensions;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Requests.Menu.ListCreatedGameSessions
{
    public class ListCreatedGameSessionsHandler :
        IRequestHandler<ListCreatedGameSessionsRequest, IResponse<ListCreatedGameSessionsResponse>>
    {
        private readonly IGameSessionRepository _gameSessionsRepository;

        public ListCreatedGameSessionsHandler(IGameSessionRepository gameSessionsRepository)
        {
            _gameSessionsRepository = gameSessionsRepository;
        }

        public async Task<IResponse<ListCreatedGameSessionsResponse>> Handle(
            ListCreatedGameSessionsRequest request,
            CancellationToken cancellationToken)
        {
            var limit = request.Limit is <= 0 or > 50 ? 10 : request.Limit;

            var gameSessions = await _gameSessionsRepository.GetTopAsync(request.Offset, limit);

            var listedGameSessions = gameSessions.Select(
                gs => new ListedGameSession()
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