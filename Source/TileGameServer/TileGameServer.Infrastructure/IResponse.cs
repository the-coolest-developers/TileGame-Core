using TileGameServer.Infrastructure.Enums;

namespace TileGameServer.Infrastructure
{
    public interface IResponse<out TResult>
    {
        TResult Result { get; }
        ResponseStatus Status { get; }
    }
}