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

        public void Create(GameSession session)
        {
            GameSessions.Add(session);
        }

        public Task CreateAsync(GameSession session)
        {
            Create(session);

            return Task.CompletedTask;
        }

        public void Delete(Guid id)
        {
            GameSessions.Remove(GameSessions.FirstOrDefault(t => t.Id == id));
        }

        public Task DeleteAsync(Guid id)
        {
            Delete(id);

            return Task.CompletedTask;
        }

        public GameSession Get(Guid id) => GameSessions.FirstOrDefault(t => t.Id == id);

        public Task<GameSession> GetAsync(Guid id)
        {
            var session = Get(id);

            return Task.FromResult(session);
        }

        public void Update(GameSession session)
        {
            var updatedSession = GameSessions.FirstOrDefault(t => t.Id == session.Id);

            if (updatedSession != null)
            {
                updatedSession.CreationDate = session.CreationDate;
                updatedSession.Status = session.Status;
                updatedSession.PlayerIds = session.PlayerIds;
            }
        }

        public Task UpdateAsync(GameSession session)
        {
            Update(session);

            return Task.CompletedTask;
        }

        public Task<GameSession> GetWithPlayerAsync(Guid playerId)
        {
            var session = GameSessions.FirstOrDefault(s => s.PlayerIds.Contains(playerId));

            return Task.FromResult(session);
        }

        public bool ExistsWithId(Guid id)
        {
            var exists = GameSessions.Exists(t => t.Id == id);

            return exists;
        }

        public Task<bool> ExistsWithIdAsync(Guid id)
        {
            var exists = ExistsWithId(id);

            return Task.FromResult(exists);
        }

        public bool ExistsWithPlayer(Guid playerId)
        {
            var exists = GameSessions.Exists(t => t.PlayerIds.FirstOrDefault(a => a == playerId) != default);

            return exists;
        }

        public Task<bool> ExistsWithPlayerAsync(Guid playerId)
        {
            var exists = ExistsWithPlayer(playerId);

            return Task.FromResult(exists);
        }
    }
}