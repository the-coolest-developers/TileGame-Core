using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TileGameServer.Commands.Menu;
using TileGameServer.Controllers.Base;

namespace TileGameServer.Controllers
{
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
            var command = new CreateGame.CreateGameCommand
            {
                UserId = Guid.Empty
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
        }
        
        [HttpGet("joinGame")]
        public async Task<ActionResult<Unit>> JoinGame()
        {
            var command = new JoinGame.JoinGameCommand
            {
                UserId = Guid.Empty
            };
            return await ExecuteActionAsync(await Mediator.Send(command));
        }

        [HttpGet("leaveGame")]
        public async Task<ActionResult<Unit>> Leave()
        {
            var command = new LeaveGame.LeaveGameCommand
            {
                UserId = Guid.Empty
            };
            return await ExecuteActionAsync(await Mediator.Send(command));
        }
        
        [HttpGet("quitGame")]
        public async Task<ActionResult<Unit>> QuitGame()
        {
            var command = new QuitGame.QuitGameCommand
            {
                UserId = Guid.Empty
            };
            return await ExecuteActionAsync(await Mediator.Send(command));
        }
    }
}