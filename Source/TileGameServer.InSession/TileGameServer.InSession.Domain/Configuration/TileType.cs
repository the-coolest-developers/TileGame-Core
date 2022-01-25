using System.Collections.Generic;

namespace TileGameServer.InSession.Domain.Configuration
{
    public class TileType
    {
        public int Id { get; init; }

        public IReadOnlyCollection<int> SideLandscapeTypes { get; init; }
    }
}