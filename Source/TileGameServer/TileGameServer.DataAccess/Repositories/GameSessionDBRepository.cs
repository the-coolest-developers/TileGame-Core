﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            _gameSessionContext.Players.UpdateRange(existingPlayers);

            _gameSessionContext.Players.AddRange(newPlayers);
            _gameSessionContext.SaveChanges();
        }

        public override async Task SaveChangesAsync()
        {
            var (existingPlayers, newPlayers) = GetExistingAndNewPlayers();

            _gameSessionContext.Players.UpdateRange(existingPlayers);

            await _gameSessionContext.Players.AddRangeAsync(newPlayers);
            await _gameSessionContext.SaveChangesAsync();
        }

        private (IQueryable<Player>, IEnumerable<Player>) GetExistingAndNewPlayers()
        {
            var modifiedPlayers = GetModifiedPlayers().ToList();

            var existingPlayers = GetExistingPlayers(modifiedPlayers);
            var newPlayers = modifiedPlayers.Except(existingPlayers);

            return (existingPlayers, newPlayers);
        }

        private IEnumerable<Player> GetModifiedPlayers() => GetModifiedEntities<Player>();

        private IQueryable<Player> GetExistingPlayers(IEnumerable<Player> modifiedPlayers)
        {
            var existingPlayers = _gameSessionContext.Players.Where(t => modifiedPlayers.Contains(t));

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