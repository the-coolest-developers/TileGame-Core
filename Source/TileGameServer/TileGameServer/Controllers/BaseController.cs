using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;

namespace TileGameServer.Controllers
{
    public class BaseController : ControllerBase
    {
        protected async Task<ActionResult<TResult>> ExecuteAction<TResult>(
            Func<Task<IResponse<TResult>>> action)
        {
            var response = await action();

            var result = response.Result;
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

        protected Task<ActionResult<TResult>> ExecuteAction<TResult>(
            IResponse<TResult> response)
        {
            var result = response.Result;
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

            return Task.FromResult(actionResult);
        }
    }
}