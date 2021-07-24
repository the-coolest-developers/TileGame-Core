using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.DataAccess.Repositories;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;

namespace TileGameServer.Commands.Menu
{
    public class LeaveGameSession
    {
        public class LeaveGameSessionCommand : IRequest<Response<Unit>>
        {
            public Guid AccountId { get; set; }
            public Guid SessionId { get; set; }
        }

        public class LeaveGameSessionCommandHandler
            : IRequestHandler<LeaveGameSessionCommand, Response<Unit>>
        {
            private readonly IGameSessionRepository _gameSessionsRepository;

            public LeaveGameSessionCommandHandler(IGameSessionRepository gameSessionsRepository)
            {
                _gameSessionsRepository = gameSessionsRepository;
            }

            public async Task<Response<Unit>> Handle(
                LeaveGameSessionCommand request,
                CancellationToken cancellationToken)
            {
                if (!await _gameSessionsRepository.ExistsWithPlayerAsync(request.AccountId))
                {
                    return new Response<Unit>
                    {
                        Status = ResponseStatus.Conflict
                    };
                }

                var session = await _gameSessionsRepository.GetAsync(request.SessionId);
                session.PlayerIds.Remove(request.AccountId);

                return new Response<Unit>
                {
                    Status = ResponseStatus.Success
                };
            }
        }

        public class LeaveGameSessionRequest
        {
            public Guid SessionId { get; set; }
        }
    }
}