using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TileGameServer.BaseLibrary.DataAccess.Context;
using TileGameServer.BaseLibrary.Domain.Entities;
using TileGameServer.BaseLibrary.Domain.Enums;
using WebApiBaseLibrary.DataAccess.Repositories;

namespace TileGameServer.DataAccess.Repositories.GameSessions
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
            var sessionWithPlayers = _gameSessionContext.GameSessions.Include(gs => gs.Players);

            return sessionWithPlayers.FirstOrDefault(gs => gs.Id == id);
        }

        public override async Task<GameSession> GetAsync(Guid id)
        {
            var sessionWithPlayers = _gameSessionContext.GameSessions.Include(gs => gs.Players);

            return await sessionWithPlayers.FirstOrDefaultAsync(gs => gs.Id == id);
        }

        public GameSession GetWithPlayer(Guid playerId, params GameSessionStatus[] statuses)
        {
            var gameSession = _gameSessionContext.GameSessions.Include(gs => gs.Players)
                .FirstOrDefault(session => session.Players.FirstOrDefault(p => p.Id == playerId) != default &&
                                           statuses.Contains(session.Status));

            return gameSession;
        }

        public Task<IEnumerable<GameSession>> GetTopAsync(int offset, int limit)
        {
            IEnumerable<GameSession> gameSessions = _gameSessionContext.GameSessions.Include(gs => gs.Players)
                .OrderBy(gs => gs.CreationDate).Skip(offset).Take(limit);

            return Task.FromResult(gameSessions);
        }

        public async Task<GameSession> GetWithPlayerAsync(Guid playerId, params GameSessionStatus[] statuses)
        {
            var gameSession = await _gameSessionContext.GameSessions.Include(gs => gs.Players)
                .FirstOrDefaultAsync(session => session.Players.FirstOrDefault(p => p.Id == playerId) != default &&
                                                statuses.Contains(session.Status));

            return gameSession;
        }

        public GameSession GetWithPlayerInOpenSessions(Guid playerId)
        {
            var gameSession = GetWithPlayer(
                playerId,
                GameSessionStatus.Created,
                GameSessionStatus.Running);

            return gameSession;
        }

        public Task<GameSession> GetWithPlayerInOpenSessionsAsync(Guid playerId)
        {
            var gameSession = GetWithPlayerAsync(
                playerId,
                GameSessionStatus.Created,
                GameSessionStatus.Running);

            return gameSession;
        }

        public override void SaveChanges()
        {
            var (existingPlayers, newPlayers) = GetExistingAndNewPlayers();

            _gameSessionContext.SessionPlayers.UpdateRange(existingPlayers);

            _gameSessionContext.SessionPlayers.AddRange(newPlayers);
            _gameSessionContext.SaveChanges();
        }

        public override async Task SaveChangesAsync()
        {
            var (existingPlayers, newPlayers) = GetExistingAndNewPlayers();

            _gameSessionContext.SessionPlayers.UpdateRange(existingPlayers);

            await _gameSessionContext.SessionPlayers.AddRangeAsync(newPlayers);
            await _gameSessionContext.SaveChangesAsync();
        }

        private (IQueryable<SessionPlayer>, IEnumerable<SessionPlayer>) GetExistingAndNewPlayers()
        {
            var modifiedPlayers = GetModifiedPlayers().ToList();

            var existingPlayers = GetExistingPlayers(modifiedPlayers);
            var newPlayers = modifiedPlayers.Except(existingPlayers);

            return (existingPlayers, newPlayers);
        }

        private IEnumerable<SessionPlayer> GetModifiedPlayers() => GetModifiedEntities<SessionPlayer>();

        private IQueryable<SessionPlayer> GetExistingPlayers(IEnumerable<SessionPlayer> modifiedPlayers)
        {
            var existingPlayers = _gameSessionContext.SessionPlayers.Where(t => modifiedPlayers.Contains(t));

            return existingPlayers;
        }

        public bool ExistsWithPlayerInOpenSessions(Guid playerId)
        {
            var player = GetWithPlayerInOpenSessions(playerId);
            bool existsWithPlayer = player != null;

            return existsWithPlayer;
        }

        public async Task<bool> ExistsWithPlayerInOpenSessionsAsync(Guid playerId)
        {
            var player = await GetWithPlayerInOpenSessionsAsync(playerId);
            bool existsWithPlayer = player != null;

            return existsWithPlayer;
        }
    }
}