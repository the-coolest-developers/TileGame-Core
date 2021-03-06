using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TileGameServer.Features.Players.GetPlayerProfile;
using TileGameServer.Features.Players.RegisterPlayer;
using WebApiBaseLibrary.Authorization.Constants;
using WebApiBaseLibrary.Authorization.Extensions;
using WebApiBaseLibrary.Controllers;

namespace TileGameServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("players")]
    public class PlayerController : BaseMediatorController
    {
        private Guid AccountId => Guid.Parse(User.GetClaim(WebApiClaimTypes.AccountId).Value);

        public PlayerController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("register")]
        public async Task<ActionResult<Unit>> Register(RegisterPlayerDto registerPlayerDto)
        {
            var command = new RegisterPlayerCommand
            {
                PlayerId = AccountId,
                PlayerNickname = registerPlayerDto.Nickname
            };

            return await SendToMediatorAsync(command);
        }

        [HttpGet("profile")]
        public Task<ActionResult<GetPlayerProfileResponse>> GetPlayerProfile()
        {
            var query = new GetPlayerProfileQuery
            {
                PlayerId = AccountId
            };

            return SendToMediatorAsync(query);
        }
    }
}