using System;
using System.Threading.Tasks;
using TileGameServer.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace TileGameServer.DataAccess.Repositories
{
    public class GameSessionRepository : IGameSessionRepository
    {
        private List<GameSession> GameSessions { get; } = new();

        public Task CreateAsync(GameSession session)
        {
            GameSessions.Add(session);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            GameSessions.Remove(GameSessions.FirstOrDefault(t => t.Id == id));

            return Task.CompletedTask;
        }

        public Task<bool> ExistsWithIdAsync(Guid id)
        {
            var exists = GameSessions.Exists(t => t.Id == id);

            return Task.FromResult(exists);
        }

        public Task<GameSession> GetWithPlayerAsync(Guid playerId)
        {
            var session = GameSessions.FirstOrDefault(s => s.PlayerIds.Contains(playerId));

            return Task.FromResult(session);
        }

        public async Task<bool> ExistsWithPlayerAsync(Guid playerId)
        {
            var session = await GetWithPlayerAsync(playerId);
            var exists = session != default;

            return exists;
        }

        public Task<GameSession> GetAsync(Guid id)
        {
            var session = GameSessions.FirstOrDefault(t => t.Id == id);

            return Task.FromResult(session);
        }

        public Task UpdateAsync(GameSession session)
        {
            var updatedSession = GameSessions.FirstOrDefault(t => t.Id == session.Id);

            if (updatedSession != null)
            {
                updatedSession.CreationDate = session.CreationDate;
                updatedSession.Status = session.Status;
                updatedSession.PlayerIds = session.PlayerIds;
            }

            return Task.CompletedTask;
        }
    }
}