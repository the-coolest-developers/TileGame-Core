using TileGameServer.Infrastructure.Enums;

namespace TileGameServer.Infrastructure.Models.Dto.Responses
{
    public interface IResponse<out TResult>
    {
        TResult Result { get; }
        ResponseStatus Status { get; }
    }
}