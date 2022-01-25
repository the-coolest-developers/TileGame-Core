using TileGameServer.InSession.Domain.Entities;
using TileGameServer.InSession.Domain.Library;

namespace TileGameServer.InSession.Domain.Creators
{
    public class TileFieldFactory : ITileFieldFactory
    {
        private readonly ITileLibrary _tileLibrary;

        public TileFieldFactory(ITileLibrary tileLibrary)
        {
            _tileLibrary = tileLibrary;
        }

        public ITileField CreateTileField(FieldSize fieldSize)
        {
            var rootTile = _tileLibrary.GetTile(0);

            var rootTilePosition = new TilePosition
            {
                X = fieldSize.Width / 2,
                Y = fieldSize.Height / 2
            };

            var tileField = new TileField(rootTile, rootTilePosition, fieldSize);
            return tileField;
        }
    }
}