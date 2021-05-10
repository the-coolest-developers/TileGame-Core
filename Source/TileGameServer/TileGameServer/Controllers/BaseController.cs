using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses;

namespace TileGameServer.Controllers
{
    public class BaseController : ControllerBase
    {
        protected async Task<ActionResult<TResult>> ExecuteAction<TResponse, TResult>(
                Func<Task<TResponse>> action)
            //Func<TResponse, TResult> resultAction)
            where TResponse : IResponse<object>
        {
            var response = await action();

            var result = response.Result;
            //var result = resultAction(response);

            ActionResult<TResult> actionResult = response.Status switch
            {
                ResponseStatus.InternalServerError => StatusCode(StatusCodes.Status500InternalServerError, result),
                ResponseStatus.Success => Ok(result),
                ResponseStatus.BadRequest => BadRequest(result),
                ResponseStatus.Conflict => Conflict(result),
                ResponseStatus.NoContent => StatusCode(StatusCodes.Status204NoContent, result),
                ResponseStatus.NotFound => NotFound(result),
                ResponseStatus.Unauthorized => Unauthorized(result),
                ResponseStatus.Accepted => Accepted(result),
                ResponseStatus.PartialContent => StatusCode(StatusCodes.Status206PartialContent, result),
                ResponseStatus.Forbidden => StatusCode(StatusCodes.Status403Forbidden, result),
                ResponseStatus.Created => StatusCode(StatusCodes.Status201Created, result),
                ResponseStatus.TooManyRequests => StatusCode(StatusCodes.Status429TooManyRequests),
                _ => throw new ArgumentOutOfRangeException(
                    $"{response.Status}",
                    "Should be a valid HTTP Status Code")
            };

            return actionResult;
        }

        /*protected async Task<ActionResult<TResult>> ExecuteAction<TResponse, TResult>(
            Func<Task<TResponse>> action)
            where TResponse : IResponse<TResult>
        {
            var a = await ExecuteAction(action, response => response.Result);
        }*/
    }
}