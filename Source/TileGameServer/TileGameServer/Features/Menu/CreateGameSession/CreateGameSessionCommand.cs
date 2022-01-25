using System;
using MediatR;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Features.Menu.CreateGameSession
{
    public class CreateGameSessionCommand : IRequest<Response<CreateGameSessionResponse>>
    {
        public Guid AccountId { get; set; }
        public int SessionCapacity { get; set; }
    }
}
