using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TileGameServer.BaseLibrary.Domain.Enums;
using TileGameServer.DataAccess.Entities;
using WebApiBaseLibrary.DataAccess.Repositories;

namespace TileGameServer.DataAccess.Repositories.GameSessions
{
    public interface IGameSessionRepository : IRepository<GameSession>, IDatabaseRepository
    {
        public Task<IEnumerable<GameSession>> GetTopAsync(int offset, int limit);

        public Task<GameSession> GetWithPlayerAsync(Guid playerId, params GameSessionStatus[] statuses);

        public GameSession GetWithPlayerInOpenSessions(Guid playerId);
        public Task<GameSession> GetWithPlayerInOpenSessionsAsync(Guid playerId);

        public bool ExistsWithPlayerInOpenSessions(Guid playerId);
        public Task<bool> ExistsWithPlayerInOpenSessionsAsync(Guid playerId);
    }
}