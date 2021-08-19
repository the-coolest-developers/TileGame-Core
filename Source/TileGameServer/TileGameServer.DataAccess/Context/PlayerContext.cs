using Microsoft.EntityFrameworkCore;
using TileGameServer.BaseLibrary.DataAccess.Context;
using TileGameServer.BaseLibrary.DataAccess.EntityConfigurations;
using TileGameServer.BaseLibrary.Domain.Entities;
using WebApiBaseLibrary.DataAccess;

namespace TileGameServer.DataAccess.Context
{
    public class PlayerContext : BaseContext
    {
        public DbSet<Player> Players { get; set; }

        public PlayerContext(
            DbContextOptions<BaseContext> options,
            IEntityConfigurationAssembly assemblyWithConfigurations = null) : base(options, assemblyWithConfigurations)
        {
        }
    }
}