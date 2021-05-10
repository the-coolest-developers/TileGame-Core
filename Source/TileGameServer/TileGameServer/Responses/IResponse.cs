using TileGameServer.Infrastructure.Enums;

namespace TileGameServer.Responses
{
    public interface IResponse<out TResult>
    {
        TResult Result { get; }
        ResponseStatus Status { get; }
    }
}