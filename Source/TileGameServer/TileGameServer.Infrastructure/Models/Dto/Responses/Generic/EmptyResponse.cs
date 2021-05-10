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
    }
}