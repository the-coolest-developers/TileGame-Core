using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;
namespace TileGameServer.Commands.Menu
{
    public class LeaveGame
    {
        public class LeaveGameCommand : IRequest<LeaveGameResponse>
        {
            public Guid UserId;
        }

        public class LeaveGameCommandHandler : IRequestHandler<LeaveGameCommand, LeaveGameResponse>
        {
            public Task<LeaveGameResponse> Handle(LeaveGameCommand request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new LeaveGameResponse
                {
                    Status = ResponseStatus.Success
                });
            }
        }

        public class LeaveGameResponse : IResponse<Unit>
        {
            public Unit Result { get; }
            public ResponseStatus Status { get; set; }
        }
    }
}