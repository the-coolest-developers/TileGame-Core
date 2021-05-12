using TileGameServer.Infrastructure.Enums;

namespace TileGameServer.Infrastructure.Models.Dto.Responses.Generic
{
    public class Response<T> : IResponse<T>
    {
        public T Result { get; set; }
        public ResponseStatus Status { get; set; }

        public static Response<TResult> Success<TResult>(TResult result)
            => new()
            {
                Result = result,
                Status = ResponseStatus.Success
            };

        public static Response<TResult> Forbidden<TResult>(TResult result) =>
            new()
            {
                Result = result,
                Status = ResponseStatus.Forbidden
            };

        public static Response<TResult> Conflict<TResult>(TResult result) =>
            new()
            {
                Result = result,
                Status = ResponseStatus.Conflict
            };
    }
}