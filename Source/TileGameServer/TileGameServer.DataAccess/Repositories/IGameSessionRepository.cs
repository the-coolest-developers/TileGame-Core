using System;
using System.Threading.Tasks;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.Enums;
using TileGameServer.DataAccess.Repositories.Generic;

namespace TileGameServer.DataAccess.Repositories
{
    public interface IGameSessionRepository : IRepository<GameSession>
    {
        Task<GameSessionStatus> GetStatus(Guid gameId);
        Task<GameSession[]> GetRecentSessionsWithStatus(GameSessionStatus gameSessionStatus, int limit = 10);
    }
}