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
        List<GameSession> GameSessions { get; set; } = new List<GameSession>();  
        public async Task CreateAsync(GameSession session)
            => await Task.Run(() => GameSessions.Add(session));
        
        public async Task DeleteAsync(Guid id) => await Task.Run(() 
            => GameSessions.Remove(GameSessions.FirstOrDefault(t => t.Id == id)));

        public async Task<bool> ExistsWithIdAsync(Guid id) 
        {
            bool IsTrue = await Task.Run(() => GameSessions.Exists(t => t.Id == id));
            return IsTrue;
        }

        public async Task<bool> ExistsWithPlayerAsync(Guid playerId)
            => await Task.Run(() => 
            GameSessions.Exists(t 
            => t.PlayerIds.FirstOrDefault(a => a == playerId) != null));
        
        public async Task<GameSession> GetAsync(Guid id) => await Task.Run(() 
            => GameSessions.FirstOrDefault(t => t.Id == id));

        public async Task UpdateAsync(GameSession session)
        {
            GameSession updatedSession = await Task.Run(() 
            => GameSessions.FirstOrDefault(t => t.Id == session.Id));
            
            if(updatedSession != null)
            {
                updatedSession.CreationDate = session.CreationDate;
                updatedSession.Status = session.Status;
                updatedSession.PlayerIds = session.PlayerIds;
            }
        }
    }
}