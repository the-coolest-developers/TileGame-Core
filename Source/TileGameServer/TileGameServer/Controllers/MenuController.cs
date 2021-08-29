﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TileGameServer.Commands.Menu.CreateGameSession;
using TileGameServer.Commands.Menu.JoinGameSession;
using TileGameServer.Commands.Menu.LeaveGameSession;
using TileGameServer.Infrastructure.MessageQueueing;
using TileGameServer.Requests.Menu.ListCreatedGameSessions;
using WebApiBaseLibrary.Authorization.Constants;
using WebApiBaseLibrary.Authorization.Extensions;
using WebApiBaseLibrary.Controllers;
using WebApiBaseLibrary.Enums;

namespace TileGameServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("menu")]
    public class MenuController : BaseMediatorController
    {
        private readonly IMessageQueuePublisher _messageQueuePublisher;

        private Guid AccountId => Guid.Parse(User.GetClaim(WebApiClaimTypes.AccountId).Value);

        public MenuController(IMediator mediator, IMessageQueuePublisher messageQueuePublisher) : base(mediator)
        {
            _messageQueuePublisher = messageQueuePublisher;
        }

        [HttpPost("createGame")]
        public async Task<ActionResult<CreateGameSessionResponse>> CreateGame(
            [FromBody] CreateGameSessionRequest request)
        {
            var command = new CreateGameSessionCommand
            {
                AccountId = AccountId,
                SessionCapacity = request.SessionCapacity
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
        }

        [HttpPost("joinGame")]
        public async Task<ActionResult<JoinGameSessionResponse>> JoinGame(
            [FromBody] JoinGameSessionRequest request)
        {
            var command = new JoinGameSessionCommand
            {
                AccountId = AccountId,
                SessionId = request.SessionId
            };

            var response = await Mediator.Send(command);
            if (response.Status == ResponseStatus.Success)
            {
                _messageQueuePublisher.PublishMessage("AAaaa");
            }

            return await ExecuteActionAsync(await Mediator.Send(command));
        }

        [HttpPost("leaveGame")]
        public async Task<ActionResult<Unit>> Leave()
        {
            var command = new LeaveGameSessionCommand
            {
                AccountId = AccountId
            };

            return await ExecuteActionAsync(await Mediator.Send(command));
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