using TileGameServer.InSession.Domain.Entities;

namespace TileGameServer.InSession.Features.GetTileField;

public class GetTileFieldResponse
{
    public Tile[][] PlacedTiles { get; init; }
}