﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TileGameServer.Commands.Menu;
using TileGameServer.Controllers.Base;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;
using TileGameServer.Extensions;

namespace TileGameServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("menu")]
    public class MenuController : BaseMediatorController
    {
        private Guid AccountId => Guid.Parse(User.GetClaim(ApplicationClaimTypes.AccountId).Value);

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
        public async Task<ActionResult<Unit>> Leave(
            [FromBody] LeaveGameSession.LeaveGameSessionRequest request)
        {
            var command = new LeaveGameSession.LeaveGameSessionCommand
            {
                AccountId = AccountId,
                SessionId = request.SessionId
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
        }
    }
}