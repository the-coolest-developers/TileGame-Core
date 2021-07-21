using System;
using System.Threading.Tasks;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.Repositories.Generic;

namespace TileGameServer.DataAccess.Repositories
{
    public interface IGameSessionRepository : IRepository<GameSession>
    {
        public Task<bool> ExistsWithPlayerAsync(Guid playerId);
    }
}