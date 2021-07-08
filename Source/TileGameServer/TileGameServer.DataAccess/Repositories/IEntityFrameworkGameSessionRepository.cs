using System;
using System.Threading.Tasks;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.Repositories.Generic;

namespace TileGameServer.DataAccess.Repositories
{
    public interface IEntityFrameworkGameSessionRepository : IRepository<GameSession>
    {
        public Task SaveChangesAsync();
    }
}