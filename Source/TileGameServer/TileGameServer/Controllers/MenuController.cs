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
            var command = new CreateGameSession.CreateGameSessionCommand
            {
                UserId = Guid.Empty
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
        }
    }
}