using System;
using System.Linq;
using System.Threading.Tasks;
using TileGameServer.DataAccess.Context;
using TileGameServer.DataAccess.Repositories.Generic;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Entities;

namespace TileGameServer.DataAccess.Repositories
{
    public class GameGameSessionRepository : EntityFrameworkBaseRepository<GameSession>, IGameSessionRepository
    {
        private GameSessionContext GameSessionContext { get; }

        public GameGameSessionRepository(GameSessionContext gameSessionContext) : base(gameSessionContext)
        {
            GameSessionContext = gameSessionContext;
        }

        public async Task<GameSessionStatus> GetStatus(Guid gameId)
        {
            var session = await GetAsync(gameId);

            return session.Status;
        }

        public Task<GameSession[]> GetRecentSessionsWithStatus(GameSessionStatus gameSessionStatus, int limit = 10)
        {
            var sessions = EntityDbSet.Where(session => session.Status == gameSessionStatus)
                .OrderBy(option => option.CreationDate).Take(limit).ToArray();

            return Task.FromResult(sessions);
        }
    }
}