using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TileGameServer.BaseLibrary.DataAccess.EntityConfigurations;
using TileGameServer.BaseLibrary.Domain.Entities;
using WebApiBaseLibrary.DataAccess;
using WebApiBaseLibrary.DataAccess.Context;

namespace TileGameServer.BaseLibrary.DataAccess.Context
{
    public class GameSessionContext : BaseContext
    {
        public DbSet<GameSession> GameSessions { get; set; }
        public DbSet<SessionPlayer> SessionPlayers { get; set; }

        //private readonly Assembly _configurationAssembly;

        public GameSessionContext(
            DbContextOptions<BaseContext> options,
            IEntityConfigurationAssembly assemblyWithConfigurations = null) : base(
             options, assemblyWithConfigurations)
        {
            //_configurationAssembly = assemblyWithConfigurations?.GetConfigurationAssembly();
        }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assembly = Assembly.GetAssembly(GetType());
            modelBuilder.ApplyConfigurationsFromAssembly(assembly!);

            if (_configurationAssembly != null)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(_configurationAssembly!);
            }
        }*/
    }
    
    public class BaseContext : DbContext
    {
        private readonly Assembly _entityConfigurationAssembly;

        public BaseContext(
            DbContextOptions<BaseContext> options,
            IEntityConfigurationAssembly entityConfigurationAssembly = null)
            : base((DbContextOptions) options)
        {
            this._entityConfigurationAssembly = entityConfigurationAssembly?.GetEntityConfigurationAssembly();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Assembly assembly = Assembly.GetAssembly(this.GetType());
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            if (!(this._entityConfigurationAssembly != (Assembly) null))
                return;
            modelBuilder.ApplyConfigurationsFromAssembly(this._entityConfigurationAssembly);
        }
    }
}