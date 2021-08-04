using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TileGameServer.BaseLibrary.DataAccess.Context;
using TileGameServer.BaseLibrary.Domain.Entities;
using WebApiBaseLibrary.DataAccess.Repositories;
using TileGameServer.BaseLibrary.Domain.Enums;

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

        public GameSession GetWithPlayer(Guid playerId, params GameSessionStatus[] statuses)
        {
            var gameSession = _gameSessionContext.GameSessions.Include(gs => gs.Players)
                .FirstOrDefault(session => session.Players.FirstOrDefault(p => p.Id == playerId) != default &&
                                           statuses.Contains(session.Status));

            return gameSession;
        }

        public async Task<GameSession> GetWithPlayerAsync(Guid playerId, params GameSessionStatus[] statuses)
        {
            var gameSession = await _gameSessionContext.GameSessions.Include(gs => gs.Players)
                .FirstOrDefaultAsync(session => session.Players.FirstOrDefault(p => p.Id == playerId) != default &&
                                           statuses.Contains(session.Status));

            return gameSession;
        }

        public GameSession GetWithPlayerFromAllSessions(Guid playerId) 
        {
            var session = GetWithPlayer(playerId, GameSessionStatus.Running,
                                                      GameSessionStatus.Created,
                                                      GameSessionStatus.Closed);
            
            return session;
        }

        public async Task<GameSession> GetWithPlayerFromAllSessionsAsync(Guid playerId)
        {
            var session = await GetWithPlayerAsync(playerId, GameSessionStatus.Running,
                                                      GameSessionStatus.Created,
                                                      GameSessionStatus.Closed);

            return session;
        }

        public override void SaveChanges()
        {
            var modifiedPlayers = _gameSessionContext.ChangeTracker.Entries()
                .Where(entry => entry.Entity is Player)
                .Where(entry => entry.State == EntityState.Modified)
                .Select(entry => entry.Entity as Player).ToList();

            var existingPlayers = _gameSessionContext.Players.Where(t => modifiedPlayers.Contains(t));
            var newPlayers = modifiedPlayers.Except(existingPlayers);

            _gameSessionContext.Players.UpdateRange(existingPlayers);
            _gameSessionContext.Players.AddRange(newPlayers);
            base.SaveChanges();
        }

        public override async Task SaveChangesAsync()
        {
            SaveChanges();

            await base.SaveChangesAsync();
        }
    }
}