using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TileGameServer.Features.Menu.CreateGameSession;
using TileGameServer.Features.Menu.JoinGameSession;
using TileGameServer.Features.Menu.LeaveGameSession;
using TileGameServer.Features.Menu.ListCreatedGameSessions;
using TileGameServer.Features.Menu.Notifications.CreateGameSession;
using TileGameServer.Features.Menu.Notifications.JoinGameSession;
using TileGameServer.Features.Menu.Notifications.LeaveGameSession;
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
        public async Task<ActionResult<CreateGameSessionResponse>> CreateGame(
            [FromBody] CreateGameSessionDto dto)
        {
            var command = new CreateGameSessionCommand
            {
                AccountId = AccountId,
                SessionCapacity = dto.SessionCapacity
            };
            var response = await Mediator.Send(command);

            var createGameNotificationCommand = new CreateGameSessionNotificationCommand
            {
                ResponseStatus = response.Status,
                GameSessionId = response.Result.SessionId
            };
            await Mediator.Send(createGameNotificationCommand);

            return await ExecuteActionAsync(response);
        }

        [HttpPost("joinGame")]
        public async Task<ActionResult<Unit>> JoinGame(
            [FromBody] JoinGameSessionRequest request)
        {
            var command = new JoinGameSessionCommand
            {
                AccountId = AccountId,
                SessionId = request.GameSessionId
            };
            var response = await Mediator.Send(command);

            var joinGameNotificationCommand = new JoinGameSessionNotificationCommand
            {
                ResponseStatus = response.Status,
                PlayerId = AccountId,
                GameSessionId = request.GameSessionId
            };
            await Mediator.Send(joinGameNotificationCommand);

            return await ExecuteActionAsync(response);
        }

        [HttpGet("leaveGame")]
        public async Task<ActionResult<Unit>> Leave()
        {
            var command = new LeaveGameSessionCommand
            {
                AccountId = AccountId
            };

            var response = await Mediator.Send(command);

            var leaveGameNotificationCommand = new LeaveGameSessionNotificationCommand
            {
                ResponseStatus = response.Status,
                PlayerId = AccountId
            };
            await Mediator.Send(leaveGameNotificationCommand);

            return await ExecuteActionAsync(response);
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