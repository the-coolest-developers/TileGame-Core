using Microsoft.EntityFrameworkCore;
using TileGameServer.BaseLibrary.Domain.Entities;
using WebApiBaseLibrary.DataAccess.Repositories;

namespace TileGameServer.DataAccess.Repositories.Players
{
    public class PlayerRepository : EntityFrameworkBaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(DbContext entityContext) : base(entityContext)
        {
        }
    }
}