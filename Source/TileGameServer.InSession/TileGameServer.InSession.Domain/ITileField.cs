using TileGameServer.InSession.Domain.Entities;

namespace TileGameServer.InSession.Domain
{
    public interface ITileField
    {
        public TilePosition RootTilePosition { get; }
        public void PlaceTile(Tile tile, TilePosition tilePosition);

        public Tile[][] GetPlacedTiles();
    }
}