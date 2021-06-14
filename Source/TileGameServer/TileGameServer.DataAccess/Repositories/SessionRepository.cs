using System;
using System.Threading.Tasks;
using TileGameServer.Enums;
using TileGameServer.Infrastructure.Models.Entities;

namespace TileGameServer.DataAccess.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        public SessionRepository()
        {
        }

        public Task CreateAsync(Session entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Session entity)
        {
            throw new NotImplementedException();
        }

        public Task<Session> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsWithIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public SessionStatus GetSessionStatus(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public Session[] GetAllSessionsWithStatus(SessionStatus sessionStatus)
        {
            throw new NotImplementedException();
        }
    }
}