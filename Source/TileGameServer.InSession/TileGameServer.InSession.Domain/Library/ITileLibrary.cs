using TileGameServer.InSession.Domain.Entities;
using TileGameServer.InSession.Domain.Enums;

namespace TileGameServer.InSession.Domain.Library
{
    public interface ITileLibrary
    {
        Tile GetTile(int tileTypeId, TileRotation tileRotation = TileRotation.Up);
    }
}