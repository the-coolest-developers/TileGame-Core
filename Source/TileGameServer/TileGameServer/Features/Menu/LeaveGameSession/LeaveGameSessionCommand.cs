using System;
using MediatR;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Features.Menu.LeaveGameSession
{
    public class LeaveGameSessionCommand : IRequest<Response<Unit>>
    {
        public Guid AccountId { get; set; }
    }
}
