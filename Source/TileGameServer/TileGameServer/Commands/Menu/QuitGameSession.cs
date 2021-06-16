using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;
namespace TileGameServer.Commands.Menu
{
    public class QuitGameSession
    {
        public class QuitGameSessionCommand : IRequest<QuitGameSessionResponse>
        {
            public Guid UserId;
        }

        public class LeaveGameCommandHandler : IRequestHandler<QuitGameSessionCommand, QuitGameSessionResponse>
        {
            public Task<QuitGameSessionResponse> Handle(QuitGameSessionCommand request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new QuitGameSessionResponse
                {
                    Status = ResponseStatus.Success
                });
            }
        }

        public class QuitGameSessionResponse : IResponse<Unit>
        {
            public Unit Result { get; }
            public ResponseStatus Status { get; set; }
        }
    }
}