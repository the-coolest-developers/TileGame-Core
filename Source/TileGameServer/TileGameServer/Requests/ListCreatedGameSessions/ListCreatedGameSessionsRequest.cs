using MediatR;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Requests.ListCreatedGameSessions
{
    public class ListCreatedGameSessionsRequest : IRequest<IResponse<ListCreatedGameSessionsResponse>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}