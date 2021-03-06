using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TileGameServer.DataAccess.Entities;

namespace TileGameServer.DataAccess.Context
{
    public class PlayerContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public PlayerContext(DbContextOptions<PlayerContext> options) : base(options)
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