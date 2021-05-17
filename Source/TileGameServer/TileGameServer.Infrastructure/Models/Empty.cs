namespace TileGameServer.Infrastructure.Models
{
    public class Empty
    {
        public static Empty Instance { get; } = new();

        private Empty()
        {
        }
    }
}