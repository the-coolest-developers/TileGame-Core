using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Dto.Responses.Generic;

namespace TileGameServer.Infrastructure.Extensions
{
    public static class ResponseExtensions
    {
        public static Response<T> GetResponse<T>(this T result, ResponseStatus status)
            => new()
            {
                Result = result,
                Status = status
            };
    }
}