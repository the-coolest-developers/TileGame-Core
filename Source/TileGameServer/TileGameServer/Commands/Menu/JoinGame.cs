using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;
using TileGameServer.Infrastructure.Models.Dto.Responses.Menu;

namespace TileGameServer.Commands.Menu
{
    public class JoinGame
    {
        public class JoinGameCommand : IRequest<JoinGameResponse>
        {
            public Guid UserId { get; set; }
        }

        public class JoinGameCommandHandler : IRequestHandler<JoinGameCommand, JoinGameResponse>
        {
            public Task<JoinGameResponse> Handle(JoinGameCommand request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new JoinGameResponse
                {
                    Status = ResponseStatus.Success
                });
            }
        }

        public class JoinGameResponse : IResponse<Unit>
        {
            public Unit Result { get; }
            public ResponseStatus Status { get; set; }
        }
    }
}