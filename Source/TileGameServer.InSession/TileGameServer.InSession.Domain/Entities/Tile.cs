namespace TileGameServer.InSession.Domain.Entities
{
    public class Tile
    {
        public int Type { get; init; }

        public int Top { get; init; }
        public int Right { get; init; }
        public int Bottom { get; init; }
        public int Left { get; init; }
    }
}