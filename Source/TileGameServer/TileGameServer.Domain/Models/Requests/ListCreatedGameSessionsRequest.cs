using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileGameServer.Domain.Models.Responses;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Domain.Models.Requests
{
    public class ListCreatedGameSessionsRequest : IRequest<IResponse<ListCreatedGameSessionsResponse>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
