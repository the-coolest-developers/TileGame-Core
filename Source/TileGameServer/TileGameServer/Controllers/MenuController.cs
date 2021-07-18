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
        public MenuController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("createGame")]
        public async Task<ActionResult<CreateGameSession.CreateGameSessionResponse>> CreateGame()
        {
            var userId = Guid.Parse(User.GetClaim(ApplicationClaimTypes.UserId).Value);
            var command = new CreateGameSession.CreateGameSessionCommand
            {
                UserId = userId
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
        }

        [HttpPost("joinGame")]
        public async Task<ActionResult<JoinGameSession.JoinGameSessionResponse>> JoinGame(
            [FromBody] JoinGameSession.JoinGameSessionRequest request)
        {
            var userId = Guid.Parse(User.GetClaim(ApplicationClaimTypes.UserId).Value);
            var command = new JoinGameSession.JoinGameSessionCommand
            {
                UserId = userId,
                SessionId = request.SessionId
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
        }

        [HttpPost("leaveGame")]
        public async Task<ActionResult<LeaveGameSession.LeaveGameSessionResponse>> Leave(
            [FromBody] LeaveGameSession.LeaveGameSessionRequest request)
        {
            var userId = Guid.Parse(User.GetClaim(ApplicationClaimTypes.UserId).Value);
            var command = new LeaveGameSession.LeaveGameSessionCommand
            {
                UserId = userId,
                SessionId = request.SessionId
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
        }
    }
}