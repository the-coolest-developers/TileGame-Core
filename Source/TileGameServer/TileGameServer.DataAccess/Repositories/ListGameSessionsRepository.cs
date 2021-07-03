using System;
using System.Threading.Tasks;
using TileGameServer.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace TileGameServer.DataAccess.Repositories
{
    public class ListGameSessionsRepository : IListGameSessionsRepository
    {
        List<GameSession> GameSessions { get; set; } = new List<GameSession>();  
        public async Task CreateAsync(GameSession session)
            => await Task.Run(() => GameSessions.Add(session));
        

        public async Task DeleteAsync(Guid id) => await Task.Run(() 
            => GameSessions.Remove(GameSessions.FirstOrDefault(t => t.Id == id)));

        public async Task<bool> ExistsWithIdAsync(Guid id) 
            => await Task.Run(() => GameSessions.Exists(t => t.Id == id));
        

        public async Task<GameSession> GetAsync(Guid id) => await Task.Run(() 
            => GameSessions.FirstOrDefault(t => t.Id == id));
        

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(GameSession session)
        {
            GameSession updatedSession = await Task.Run(() 
            => GameSessions.FirstOrDefault(t => t.Id == session.Id));
            
            if(updatedSession != null)
            {
                updatedSession.CreationDate = session.CreationDate;
                updatedSession.Status = session.Status;
                updatedSession.Players = session.Players;
            }
        }
    }
}