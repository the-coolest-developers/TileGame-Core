using System;
using System.Threading.Tasks;
using TileGameServer.DataAccess.Entities;
using WebApiBaseLibrary.DataAccess.Repositories;

namespace TileGameServer.DataAccess.Repositories
{
    public interface IGameSessionRepository : IRepository<GameSession>
    {
        public bool ExistsWithPlayer(Guid playerId);
        public Task<bool> ExistsWithPlayerAsync(Guid playerId);
    }
}