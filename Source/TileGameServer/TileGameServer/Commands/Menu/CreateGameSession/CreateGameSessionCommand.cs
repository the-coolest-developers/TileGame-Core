using MediatR;
using System;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Commands.Menu.CreateGameSession
{
    public class CreateGameSessionCommand : IRequest<Response<CreateGameSessionResponse>>
    {
        public Guid AccountId { get; set; }
        public int SessionCapacity { get; set; }
    }
}
