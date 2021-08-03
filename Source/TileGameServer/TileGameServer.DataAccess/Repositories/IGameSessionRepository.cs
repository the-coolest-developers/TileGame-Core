using System;
using System.Threading.Tasks;
using TileGameServer.BaseLibrary.Domain.Entities;
using WebApiBaseLibrary.DataAccess.Repositories;

namespace TileGameServer.DataAccess.Repositories
{
    public interface IGameSessionRepository : IRepository<GameSession>, IDatabaseRepository
    {
        public Task<GameSession> GetWithPlayerAsync(Guid playerId);

        public bool ExistsWithPlayer(Guid playerId);
        public Task<bool> ExistsWithPlayerAsync(Guid playerId);
    }
}