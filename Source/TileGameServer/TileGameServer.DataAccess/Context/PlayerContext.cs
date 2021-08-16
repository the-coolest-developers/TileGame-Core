using Microsoft.EntityFrameworkCore;
using TileGameServer.BaseLibrary.DataAccess.Context;
using TileGameServer.BaseLibrary.DataAccess.EntityConfigurations;
using TileGameServer.BaseLibrary.Domain.Entities;

namespace TileGameServer.DataAccess.Context
{
    public class PlayerContext : BaseDbContext
    {
        public DbSet<Player> Players { get; set; }

        public PlayerContext(
            DbContextOptions<GameSessionContext> options,
            IConfigurationAssembly assemblyWithConfigurations = null) : base(options, assemblyWithConfigurations)
        {
        }
    }
}