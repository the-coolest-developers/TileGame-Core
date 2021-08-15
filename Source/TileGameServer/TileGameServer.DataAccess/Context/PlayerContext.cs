using Microsoft.EntityFrameworkCore;
using TileGameServer.BaseLibrary.Domain.Entities;

namespace TileGameServer.DataAccess.Context
{
    public class PlayerContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
    }
}