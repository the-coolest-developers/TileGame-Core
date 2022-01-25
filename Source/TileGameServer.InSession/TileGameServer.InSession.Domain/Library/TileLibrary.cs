using System;
using System.Collections.Generic;
using System.Linq;
using TileGameServer.InSession.Domain.Configuration;
using TileGameServer.InSession.Domain.Entities;
using TileGameServer.InSession.Domain.Enums;

namespace TileGameServer.InSession.Domain.Library
{
    public class TileLibrary : ITileLibrary
    {
        private readonly IReadOnlyCollection<int> _landscapeTypes;
        private readonly IReadOnlyCollection<TileType> _tileTypes;

        private readonly IReadOnlyCollection<int> _availableTileTypeNumbers;

        public TileLibrary(TileGameConfiguration tileGameConfiguration)
        {
            _landscapeTypes = tileGameConfiguration.LandscapeTypes;
            _tileTypes = tileGameConfiguration.TileTypes;

            _availableTileTypeNumbers = _tileTypes.Select(t => t.Id).ToList().AsReadOnly();
        }

        public Tile GetTile(int tileTypeId, TileRotation tileRotation = TileRotation.Up)
        {
            if (!_availableTileTypeNumbers.Contains(tileTypeId))
            {
                throw new IndexOutOfRangeException($"No tile found with the following type: {tileTypeId}");
            }

            var landscapeTypes = _tileTypes.First(t => t.Id == tileTypeId).SideLandscapeTypes.ToArray();
            Shift(landscapeTypes, (int)tileRotation);

            var tile = new Tile
            {
                Type = tileTypeId,

                Top = landscapeTypes[0],
                Right = landscapeTypes[1],
                Bottom = landscapeTypes[2],
                Left = landscapeTypes[3]
            };

            return tile;
        }

        private void Shift(IList<int> array, int shiftDistance)
        {
            for (int i = 0; i < shiftDistance; i++)
            {
                int last = array.Last();
                for (int j = array.Count - 1; j > 0; j--)
                {
                    array[j] = array[j - 1];
                }

                array[0] = last;
            }
        }
    }
}