using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;

namespace TileGameServer.Commands.Menu
{
    public class LeaveGameSession
    {
        public class LeaveGameSessionCommand : IRequest<LeaveGameSessionResponse>
        {
            public Guid UserId { get; set; }
        }

        public class JoinGameCommandHandler : IRequestHandler<LeaveGameSessionCommand, LeaveGameSessionResponse>
        {
            public Task<LeaveGameSessionResponse> Handle(LeaveGameSessionCommand request,
                CancellationToken cancellationToken)
            {
                return Task.FromResult(new LeaveGameSessionResponse
                {
                    Status = ResponseStatus.Success
                });
            }
        }

        public class LeaveGameSessionResponse : IResponse<Unit>
        {
            public Unit Result { get; }
            public ResponseStatus Status { get; set; }
        }
    }
}