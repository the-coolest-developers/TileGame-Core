using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.DataAccess.Repositories;
using WebApiBaseLibrary.Enums;
using WebApiBaseLibrary.Responses;

namespace TileGameServer.Commands.Menu
{
    public class LeaveGameSession
    {
        public class LeaveGameSessionCommand : IRequest<Response<LeaveGameSessionResponse>>
        {
            public Guid UserId { get; set; }
            public Guid SessionId { get; set; }
        }

        public class LeaveGameSessionCommandHandler
            : IRequestHandler<LeaveGameSessionCommand, Response<LeaveGameSessionResponse>>
        {
            private readonly IGameSessionRepository _gameSessionsRepository;

            public LeaveGameSessionCommandHandler(
                IGameSessionRepository gameSessionsRepository)
            {
                _gameSessionsRepository = gameSessionsRepository;
            }

            public async Task<Response<LeaveGameSessionResponse>> Handle(LeaveGameSessionCommand request,
                CancellationToken cancellationToken)
            {
                if (await _gameSessionsRepository.ExistsWithPlayerAsync(request.UserId))
                {
                    var session = await _gameSessionsRepository.GetAsync(request.SessionId);
                    session.PlayerIds.Remove(request.UserId);

                    return new Response<LeaveGameSessionResponse>
                    {
                        Status = ResponseStatus.Success
                    };
                }

                return new Response<LeaveGameSessionResponse>
                {
                    Status = ResponseStatus.Conflict
                };
            }
        }

        public class LeaveGameSessionResponse
        {
            public Guid UserId { get; set; }
            public Guid SessionId { get; set; }
        }

        public class LeaveGameSessionRequest
        {
            public Guid SessionId { get; set; }
        }
    }
}