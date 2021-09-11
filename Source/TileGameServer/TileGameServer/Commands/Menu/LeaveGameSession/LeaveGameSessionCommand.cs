using MediatR;
using System;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Commands.Menu.LeaveGameSession
{
    public class LeaveGameSessionCommand : IRequest<Response<Unit>>
    {
        public Guid AccountId { get; set; }
    }
}
