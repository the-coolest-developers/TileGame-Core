using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;

namespace TileGameServer.Commands.Menu
{
    public class QuitGame
    {
        
        public class QuitGameCommand : IRequest<QuitGameResponse>
        {
            public Guid UserId { get; set; }
        }

        public class QuitGameCommandHandler : IRequestHandler<QuitGameCommand, QuitGameResponse>
        {
            public Task<QuitGameResponse> Handle(QuitGameCommand request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new QuitGameResponse
                {
                    Status = ResponseStatus.Success
                });
            }
        }

        public class QuitGameResponse : IResponse<Unit>
        {
            public Unit Result { get; }
            public ResponseStatus Status { get; set; }
        }
    }
}