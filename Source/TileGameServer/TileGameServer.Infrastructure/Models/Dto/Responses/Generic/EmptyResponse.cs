using TileGameServer.Infrastructure.Enums;

namespace TileGameServer.Infrastructure.Models.Dto.Responses.Generic
{
    public sealed class EmptyResponse : Response<Empty>
    {
        public EmptyResponse()
        {
            Result = Empty.Instance;
        }

        public static EmptyResponse FromStatus(ResponseStatus status) =>
            new()
            {
                Status = status
            };
        
        public static EmptyResponse Success()
            => new()
            {
                Status = ResponseStatus.Success
            };
        
        public static EmptyResponse Forbidden()
            => new()
            {
                Status = ResponseStatus.Forbidden
            };
        
        public static EmptyResponse Unauthorized()
            => new()
            {
                Status = ResponseStatus.Unauthorized
            }; 
    }
}