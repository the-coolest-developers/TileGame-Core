using Microsoft.EntityFrameworkCore;
using TileGameServer.BaseLibrary.Domain.Entities;

namespace TileGameServer.BaseLibrary.DataAccess.Context
{
    public class GameSessionContext : DbContext
    {
        public DbSet<GameSession> GameSessions { get; set; }

        public GameSessionContext(DbContextOptions<GameSessionContext> options) : base(options)
        {
        }
    }
}