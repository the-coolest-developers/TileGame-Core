using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;

namespace TileGameServer.Controllers.Base
{
    public class BaseMediatorController : BaseController
    {
        protected IMediator Mediator { get; }

        public BaseMediatorController(IMediator mediator)
        {
            Mediator = mediator;
        }

        public Task<ActionResult<TResult>> SendToMediatorAsync<TResult>(IRequest<IResponse<TResult>> request)
        {
            return ExecuteActionAsync(Mediator.Send(request));
        }
    }
}