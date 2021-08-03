using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TileGameServer.BaseLibrary.DataAccess.Context;
using TileGameServer.BaseLibrary.Domain.Entities;
using WebApiBaseLibrary.DataAccess.Repositories;

namespace TileGameServer.DataAccess.Repositories
{
    public class GameSessionDbRepository : EntityFrameworkBaseRepository<GameSession>, IGameSessionRepository
    {
        private readonly GameSessionContext _gameSessionContext;

        public GameSessionDbRepository(GameSessionContext entityContext) : base(entityContext)
        {
            _gameSessionContext = entityContext;
        }

        public override GameSession Get(Guid id)
        {
            var includableQueryable = _gameSessionContext.GameSessions.Include(gs => gs.Players);

            return includableQueryable.FirstOrDefault(gs => gs.Id == id);
        }

        public override async Task<GameSession> GetAsync(Guid id)
        {
            var includableQueryable = _gameSessionContext.GameSessions.Include(gs => gs.Players);

            return await includableQueryable.FirstOrDefaultAsync(gs => gs.Id == id);
        }

        public bool ExistsWithPlayer(Guid playerId)
        {
            var player = EntityDbSet.Find(playerId);

            if (player != null)
            {
                return true;
            }

            return false;
        }

        public Task<bool> ExistsWithPlayerAsync(Guid playerId)
        {
            var res = ExistsWithPlayer(playerId);
            return Task.FromResult(res);
        }

        public Task<GameSession> GetWithPlayerAsync(Guid playerId)
        {
            var player = Get(playerId);
            return Task.FromResult(player);
        }
    }
}