using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TileGameServer.Infrastructure.Models.Dto.Responses.Menu;
using TileGameServer.Services;

namespace TileGameServer.Controllers
{
    [ApiController]
    [Route("menu")]
    public class MenuController : BaseController
    {
        private IMenuService MenuService { get; }

        public MenuController(IMenuService menuService)
        {
            MenuService = menuService;
        }

        [HttpGet("createGame")]
        public async Task<ActionResult<CreateGameResponse>> CreateGame()
        {
            return await ExecuteAction(() => MenuService.CreateGame(new Guid()));
        }
    }
}