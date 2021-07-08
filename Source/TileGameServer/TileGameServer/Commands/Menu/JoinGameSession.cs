using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;
using TileGameServer.DataAccess.Repositories;
using TileGameServer.DataAccess.Entities;

namespace TileGameServer.Commands.Menu
{
    public class JoinGameSession
    {
        
        public class JoinGameSessionCommand : IRequest<JoinGameSessionResponse>
        {
            public Guid UserId { get; set; }
            public Guid SessionId { get; set; }
        }

        public class JoinGameSessionCommandHandler : IRequestHandler<JoinGameSessionCommand, JoinGameSessionResponse>
        {
            private IListGameSessionsRepository ListGameSessionsRepository { get; }
            public async Task<JoinGameSessionResponse> Handle(JoinGameSessionCommand request, CancellationToken cancellationToken)
            {
                if(await ListGameSessionsRepository.ExistsWithPlayerAsync(request.UserId))
                {
                    return new JoinGameSessionResponse
                    {
                        Status = ResponseStatus.Conflict
                    };                    
                }

                GameSession session = await ListGameSessionsRepository.GetAsync(request.SessionId);
                session.PlayersId.Add(request.UserId);
                
                return new JoinGameSessionResponse
                {
                    Status = ResponseStatus.Success
                };
             
            }
        }

        public class JoinGameSessionResponse : IResponse<Unit>
        {
            public Unit Result { get; }
            public ResponseStatus Status { get; set; }
        }
    }
}