using TileGameServer.Infrastructure.Enums;

namespace TileGameServer.Infrastructure.Models.Dto.Responses.Generic
{
    public class Response<T> : IResponse<T>
    {
        public T Result { get; set; }
        public ResponseStatus Status { get; set; }
    }
}