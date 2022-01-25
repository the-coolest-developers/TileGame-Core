using System;
using MediatR;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Features.Menu.JoinGameSession
{
    public class JoinGameSessionCommand : IRequest<Response<Unit>>
    {
        public Guid AccountId { get; set; }
        public Guid SessionId { get; set; }
    }
}
