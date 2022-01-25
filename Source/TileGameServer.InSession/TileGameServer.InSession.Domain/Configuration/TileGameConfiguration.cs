using System.Collections.Generic;

namespace TileGameServer.InSession.Domain.Configuration
{
    public class TileGameConfiguration
    {
        public IReadOnlyCollection<int> LandscapeTypes { get; init; }

        public IReadOnlyCollection<TileType> TileTypes { get; init; }
    }
}