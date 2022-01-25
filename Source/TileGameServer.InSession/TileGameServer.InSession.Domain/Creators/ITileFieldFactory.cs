namespace TileGameServer.InSession.Domain.Creators
{
    public interface ITileFieldFactory
    {
        ITileField CreateTileField(FieldSize fieldSize);
    }
}