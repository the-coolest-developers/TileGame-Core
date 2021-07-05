using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TileGameServer.Commands.Menu;
using TileGameServer.Controllers.Base;

namespace TileGameServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("menu")]
    public class MenuController : BaseMediatorController
    {
        public MenuController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("createGame")]
        public async Task<ActionResult<Unit>> CreateGame()
        {
            var command = new CreateGameSession.CreateGameSessionCommand
            {
                UserId = Guid.Empty
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
        }

        [HttpGet("joinGame")]
        public async Task<ActionResult<Unit>> JoinGame([FromBody] Guid SessionId)
        {
            var command = new JoinGameSession.JoinGameSessionCommand
            {
                UserId = Guid.Empty,
                SessionId = SessionId
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
        }

        [HttpGet("leaveGame")]
        public async Task<ActionResult<Unit>> Leave()
        {
            var command = new LeaveGameSession.LeaveGameSessionCommand
            {
                UserId = Guid.Empty
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
        }

        [HttpGet("quitGame")]
        public async Task<ActionResult<Unit>> QuitGame()
        {
            var command = new QuitGameSession.QuitGameSessionCommand
            {
                UserId = Guid.Empty
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
        }
    }
}