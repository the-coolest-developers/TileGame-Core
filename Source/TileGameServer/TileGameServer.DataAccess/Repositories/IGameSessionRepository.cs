using System;
using System.Threading.Tasks;
using TileGameServer.BaseLibrary.Domain.Entities;
using TileGameServer.BaseLibrary.Domain.Enums;
using WebApiBaseLibrary.DataAccess.Repositories;

namespace TileGameServer.DataAccess.Repositories
{
    public interface IGameSessionRepository : IRepository<GameSession>, IDatabaseRepository
    {
        public Task<GameSession> GetWithPlayerAsync(Guid playerId, params GameSessionStatus[] statuses);

        public bool ExistsWithPlayer(Guid playerId, params GameSessionStatus[] statuses);
        public Task<bool> ExistsWithPlayerAsync(Guid playerId, params GameSessionStatus[] statuses);
    }
}