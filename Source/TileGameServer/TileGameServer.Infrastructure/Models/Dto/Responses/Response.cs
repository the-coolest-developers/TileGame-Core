using TileGameServer.Infrastructure.Enums;

namespace TileGameServer.Infrastructure.Models.Dto.Responses
{
    public class Response<TResult> : IResponse<TResult>
    {
        public TResult Result { get; set;}
        public ResponseStatus Status { get; set; }
        
        public static Response<TResult> Success(TResult result) => 
        new Response<TResult>
        {
            Result = result,
            Status = ResponseStatus.Success
        };

        public static Response<TResult> Forbidden(TResult result) => 
        new Response<TResult>
        {
            Result = result,
            Status = ResponseStatus.Forbidden
        };

        public static Response<TResult> Conflict(TResult result) => 
        new Response<TResult>
        {
            Result = result,
            Status = ResponseStatus.Conflict
        };

    }
}