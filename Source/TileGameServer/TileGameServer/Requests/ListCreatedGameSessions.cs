using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.BaseLibrary.Domain.Entities;
using TileGameServer.DataAccess.Repositories;
using TileGameServer.Domain.Models.Requests;
using TileGameServer.Domain.Models.Responses;
using WebApiBaseLibrary.Extensions;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Requests
{
    public class ListCreatedGameSessions
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

                var response = new ListCreatedGameSessionsResponse
                {
                    GameSessions = gameSessions.ToArray()
                };

                return response.Success();
            }
        }
    }
}