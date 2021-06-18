using Microsoft.EntityFrameworkCore;
using TileGameServer.DataAccess.Entities;

namespace TileGameServer.DataAccess.Context
{
    public class GameSessionContext : DbContext
    {
        public DbSet<GameSession> GameSessions { get; set; }

        public GameSessionContext(DbContextOptions<GameSessionContext> options) : base(options)
        {
        }
    }
}