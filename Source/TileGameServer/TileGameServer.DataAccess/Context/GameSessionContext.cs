using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TileGameServer.DataAccess.Entities;

namespace TileGameServer.DataAccess.Context
{
    public class GameSessionContext : DbContext
    {
        public DbSet<GameSession> GameSessions { get; set; }
        public DbSet<SessionPlayer> SessionPlayers { get; set; }

        public GameSessionContext(DbContextOptions<GameSessionContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assembly = Assembly.GetAssembly(GetType());
            modelBuilder.ApplyConfigurationsFromAssembly(assembly!);
        }
    }
}