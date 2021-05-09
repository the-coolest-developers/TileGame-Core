using Microsoft.AspNetCore.Mvc;
using TileGameServer.Services;

namespace TileGameServer.Controllers
{
    [ApiController]
    public class MenuController : ControllerBase
    {
        private IMenuService MenuService { get; }

        public MenuController(IMenuService menuService)
        {
            MenuService = menuService;
        }
    }
}