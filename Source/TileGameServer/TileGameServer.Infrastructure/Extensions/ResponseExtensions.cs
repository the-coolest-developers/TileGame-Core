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

        public static Response<T> Success<T>(this T result)
            => new()
            {
                Result = result,
                Status = ResponseStatus.Success
            };
        
        public static Response<T> Forbidden<T>(this T result)
            => new()
            {
                Result = result,
                Status = ResponseStatus.Forbidden
            };
        
        public static Response<T> Unauthorized<T>(this T result)
            => new()
            {
                Result = result,
                Status = ResponseStatus.Unauthorized
            };
    }
}