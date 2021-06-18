using System;
using System.Threading.Tasks;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.Repositories.Generic;
using TileGameServer.Infrastructure.Enums;

namespace TileGameServer.DataAccess.Repositories
{
    public interface IGameSessionRepository : IRepository<GameSession>
    {
        Task<GameSessionStatus> GetStatus(Guid gameId);
        Task<GameSession[]> GetRecentSessionsWithStatus(GameSessionStatus gameSessionStatus, int limit = 10);
    }
}