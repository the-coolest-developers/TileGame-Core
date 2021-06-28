using Microsoft.EntityFrameworkCore;
using TileGameServer.DataAccess.Entities;

namespace TileGameServer.DataAccess.Context
{
    public class GameSessionContext : DbContext
    {
        public DbSet<AccountInSession> AccountsInSession  { get; set; }

        public GameSessionContext(DbContextOptions<GameSessionContext> options) : base(options)
        {
        }
    }
}