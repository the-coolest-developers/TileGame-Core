using System;
using System.Threading.Tasks;
using TileGameServer.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using TileGameServer.Infrastructure.Models;

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

        public async Task DeleteAsync(Guid id) => await Task.Run(()
            => GameSessions.Remove(GameSessions.FirstOrDefault(t => t.Id == id)));

        public Task<bool> ExistsWithIdAsync(Guid id)
        {
            var exists = GameSessions.Exists(t => t.Id == id);

            return Task.FromResult(exists);
        }

        public Task<bool> ExistsWithPlayerAsync(Guid playerId)
        {
            var exists = GameSessions.Exists(
                t => t.PlayerIds.FirstOrDefault(a => a == playerId) != default);

            return Task.FromResult(exists);
        }

        public async Task<GameSession> GetAsync(Guid id) => await Task.Run(()
            => GameSessions.FirstOrDefault(t => t.Id == id));

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