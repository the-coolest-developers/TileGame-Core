using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TileGameServer.Commands.Menu;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;

namespace TileGameServer.Controllers
{
    [ApiController]
    [Route("menu")]
    public class MenuController : BaseController
    {
        private IMediator Mediator { get; }

        public MenuController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet("createGame")]
        public async Task<ActionResult<Unit>> CreateGame()
        {
            var command = new CreateGame.CreateGameCommand
            {
                UserId = Guid.Empty
            };

            IResponse<Unit> response = await Mediator.Send(command);
            var t = Task.Run(() => response);
            return await ExecuteAction<Unit>(() => Mediator.Send(command));
        }
    }
}