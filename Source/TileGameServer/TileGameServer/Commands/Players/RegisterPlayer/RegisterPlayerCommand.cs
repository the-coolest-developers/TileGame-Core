using System;
using MediatR;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Commands.Players.RegisterPlayer
{
    public class RegisterPlayerCommand : IRequest<IResponse<Unit>>
    {
        public Guid PlayerId { get; set; }
        public string PlayerNickname { get; set; }
    }
}