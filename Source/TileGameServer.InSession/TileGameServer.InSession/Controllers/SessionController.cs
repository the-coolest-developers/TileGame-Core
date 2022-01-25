using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TileGameServer.InSession.Features.GetTileField;
using WebApiBaseLibrary.Controllers;

namespace TileGameServer.InSession.Controllers
{
    [Route("sessions")]
    [ApiController]
    public class SessionController : BaseMediatorController
    {
        public SessionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("test")]
        public string Test() => "sd";

        [Authorize]
        [HttpGet("{sessionId}/field")]
        public Task<ActionResult<GetTileFieldResponse>> GetTileField(Guid sessionId)
        {
            var query = new GetTileFieldQuery
            {
                SessionId = sessionId
            };

            return SendToMediatorAsync(query);
        }
    }
}