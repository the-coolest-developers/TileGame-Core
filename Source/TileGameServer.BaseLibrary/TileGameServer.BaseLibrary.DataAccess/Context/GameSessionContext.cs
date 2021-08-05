using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TileGameServer.BaseLibrary.Domain.Entities;

namespace TileGameServer.BaseLibrary.DataAccess.Context
{
    public class GameSessionContext : DbContext
    {
        public DbSet<GameSession> GameSessions { get; set; }
        public DbSet<Player> Players { get; set; }

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