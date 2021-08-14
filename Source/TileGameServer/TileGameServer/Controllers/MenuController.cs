using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TileGameServer.Commands.Menu;
using TileGameServer.Requests.ListCreatedGameSessions;
using WebApiBaseLibrary.Authorization.Constants;
using WebApiBaseLibrary.Authorization.Extensions;
using WebApiBaseLibrary.Controllers;

namespace TileGameServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("menu")]
    public class MenuController : BaseMediatorController
    {
        private Guid AccountId => Guid.Parse(User.GetClaim(WebApiClaimTypes.AccountId).Value);

        public MenuController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("createGame")]
        public async Task<ActionResult<CreateGameSession.CreateGameSessionResponse>> CreateGame(
            [FromBody] CreateGameSession.CreateGameSessionRequest request)
        {
            var command = new CreateGameSession.CreateGameSessionCommand
            {
                AccountId = AccountId,
                SessionCapacity = request.SessionCapacity
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
        }

        [HttpPost("joinGame")]
        public async Task<ActionResult<JoinGameSession.JoinGameSessionResponse>> JoinGame(
            [FromBody] JoinGameSession.JoinGameSessionRequest request)
        {
            var command = new JoinGameSession.JoinGameSessionCommand
            {
                AccountId = AccountId,
                SessionId = request.SessionId
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
        }

        [HttpPost("leaveGame")]
        public async Task<ActionResult<Unit>> Leave()
        {
            var command = new LeaveGameSession.LeaveGameSessionCommand
            {
                AccountId = AccountId
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
        }

        [HttpGet("listCreatedGameSessions/{offset:int?}/{limit:int?}")]
        public async Task<ActionResult<ListCreatedGameSessionsResponse>>
            ListCreatedGameSessions(int offset = 0, int limit = 10)
        {
            var command = new ListCreatedGameSessionsRequest
            {
                Offset = offset,
                Limit = limit
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
        }
    }
}