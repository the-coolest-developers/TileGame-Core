using System;
using System.Threading.Tasks;
using TileGameServer.DataAccess.Repositories.Generic;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Entities;

namespace TileGameServer.DataAccess.Repositories
{
    public interface IGameSessionRepository : IRepository<GameSession>
    {
        Task<GameSessionStatus> GetStatus(Guid gameId);
        Task<GameSession[]> GetRecentSessionsWithStatus(GameSessionStatus gameSessionStatus, int limit = 10);
    }
}