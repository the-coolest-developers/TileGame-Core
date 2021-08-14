using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TileGameServer.BaseLibrary.DataAccess.EntityConfigurations;
using TileGameServer.BaseLibrary.Domain.Entities;

namespace TileGameServer.BaseLibrary.DataAccess.Context
{
    public class GameSessionContext : DbContext
    {
        public DbSet<GameSession> GameSessions { get; set; }
        public DbSet<SessionPlayer> SessionPlayers { get; set; }

        private readonly Assembly _configurationAssembly;

        public GameSessionContext(
            DbContextOptions<GameSessionContext> options,
            IConfigurationAssembly assemblyWithConfigurations = null) : base(options)
        {
            _configurationAssembly = assemblyWithConfigurations?.GetConfigurationAssembly();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assembly = Assembly.GetAssembly(GetType());
            modelBuilder.ApplyConfigurationsFromAssembly(assembly!);

            if (_configurationAssembly != null)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(_configurationAssembly!);
            }
        }
    }
}