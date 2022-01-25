using System;
using TileGameServer.InSession.Domain.Entities;

namespace TileGameServer.InSession.Domain
{
    public class TileField : ITileField
    {
        public TilePosition RootTilePosition { get; }

        private readonly Tile[][] _placedTiles;

        public TileField(
            Tile rootTile,
            TilePosition rootTilePosition,
            FieldSize fieldSize)
        {
            _placedTiles = new Tile[fieldSize.Width][];
            for (int i = 0; i < _placedTiles.Length; i++)
            {
                _placedTiles[i] = new Tile[fieldSize.Height];
            }

            RootTilePosition = rootTilePosition;
            _placedTiles[rootTilePosition.X][rootTilePosition.Y] = rootTile;
        }

        public void PlaceTile(Tile tile, TilePosition tilePosition)
        {
            var selectedTileNode = _placedTiles[tilePosition.X][tilePosition.Y];
            if (selectedTileNode != null)
            {
                throw new Exception("This node is already filled with a tile!");
            }

            if (!CanPlaceTile(tile, tilePosition))
            {
                throw new Exception("Can't place the tile here!");
            }

            _placedTiles[tilePosition.X][tilePosition.Y] = tile;
        }

        public Tile[][] GetPlacedTiles()
        {
            return _placedTiles;
        }

        private bool CanPlaceTile(Tile tile, TilePosition tilePosition)
        {
            var topTile = _placedTiles[tilePosition.X][tilePosition.Y + 1];
            var rightTile = _placedTiles[tilePosition.X + 1][tilePosition.Y];
            var bottomTile = _placedTiles[tilePosition.X][tilePosition.Y - 1];
            var leftTile = _placedTiles[tilePosition.X - 1][tilePosition.Y];

            var noAdjacentTiles = topTile == null
                                  && rightTile == null
                                  && bottomTile == null
                                  && leftTile == null;

            if (noAdjacentTiles)
            {
                return false;
            }

            var sidesMatchToAdjacentTiles =
                topTile != null && topTile.Bottom != tile.Top
                || rightTile != null && rightTile.Left != tile.Left
                || bottomTile != null && bottomTile.Top != tile.Bottom
                || leftTile != null && leftTile.Right != tile.Left;

            return sidesMatchToAdjacentTiles;
        }

        private Tile GetTile(int x, int y) => _placedTiles[x][y];
        private Tile GetTile(TilePosition tilePosition) => GetTile(tilePosition.X, tilePosition.Y);
    }
}