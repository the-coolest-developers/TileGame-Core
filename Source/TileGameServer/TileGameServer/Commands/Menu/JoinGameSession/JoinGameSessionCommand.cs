using MediatR;
using System;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Commands.Menu.JoinGameSession
{
    public class JoinGameSessionCommand : IRequest<Response<JoinGameSessionResponse>>
    {
        public Guid AccountId { get; set; }
        public Guid SessionId { get; set; }
    }
}
