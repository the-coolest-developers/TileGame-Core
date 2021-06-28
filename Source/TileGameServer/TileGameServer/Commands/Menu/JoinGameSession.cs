using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;

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
            public Task<JoinGameSessionResponse> Handle(JoinGameSessionCommand request, CancellationToken cancellationToken)
            {
                var user = AccountInSessionRepository.GetAsync(request.UserId);
                
                if(user == null)
                {
                    AccountsInSession.AddAsync(new AccountsInSession
                    {
                        Id = request.UserId,
                        GameSessionId = request.SessionId
                    });
                }

                return Task.FromResult(new JoinGameSessionResponse
                {
                    Status = ResponseStatus.Success
                });
            }
        }

        public class JoinGameSessionResponse : IResponse<Unit>
        {
            public Unit Result { get; }
            public ResponseStatus Status { get; set; }
        }
    }
}