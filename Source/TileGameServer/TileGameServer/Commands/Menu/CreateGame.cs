using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.Hubs;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;

namespace TileGameServer.Commands.Menu
{
    public class CreateGame
    {
        public class CreateGameCommand : IRequest<CreateGameResponse>
        {
            public Guid UserId { get; set; }
        }

        public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, CreateGameResponse>
        {
            private MenuHub MenuHub { get; }

            public CreateGameCommandHandler(MenuHub menuHub)
            {
                MenuHub = menuHub;
            }

            public Task<CreateGameResponse> Handle(CreateGameCommand request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new CreateGameResponse
                {
                    Status = ResponseStatus.Success
                });
            }
        }

        public class CreateGameResponse : IResponse<Unit>
        {
            public Unit Result { get; }
            public ResponseStatus Status { get; set; }
        }
    }
}