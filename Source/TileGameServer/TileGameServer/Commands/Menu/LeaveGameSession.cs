using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.Enums;
using TileGameServer.DataAccess.Repositories;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Generators;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;

namespace TileGameServer.Commands.Menu
{
    public class LeaveGameSession
    {
        public class LeaveGameSessionCommand : IRequest<LeaveGameSessionResponse>
        {
            public Guid UserId { get; set; }
            public Guid SessionId { get; set; }
        }

        public class JoinGameCommandHandler : IRequestHandler<LeaveGameSessionCommand, LeaveGameSessionResponse>
        {
            private readonly IGameSessionRepository _gameSessionsRepository;
            public CreateGameSessionCommandHandler(
                IGameSessionRepository gameSessionsRepository,
                IJwtGenerator jwtGenerator
            )
            {
                _gameSessionsRepository = gameSessionsRepository;
            }

            public Task<LeaveGameSessionResponse> Handle(LeaveGameSessionCommand request,
                CancellationToken cancellationToken)
            {
                if(await _gameSessionsRepository.ExistsWithPlayerAsync(request.UserId))
                {
                    var session = _gameSessionsRepository.GetAsync(request.SessionId);
                    session.PlayerIds.Remove(request.UserId);

                    return Task.FromResult(new LeaveGameSessionResponse
                    {
                        Status = ResponseStatus.Success
                    });
                }

                return Task.FromResult(new LeaveGameSessionResponse
                {
                    Status = ResponseStatus.Conflict
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