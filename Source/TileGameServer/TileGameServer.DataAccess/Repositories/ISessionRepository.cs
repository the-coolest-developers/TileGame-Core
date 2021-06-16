using System;
using TileGameServer.DataAccess.Repositories.Generic;
using TileGameServer.Enums;
using TileGameServer.Infrastructure.Models.Entities;

namespace TileGameServer.DataAccess.Repositories
{
    public interface ISessionRepository : IRepository<Session>
    {
        SessionStatus GetSessionStatus(Guid gameId);
        Session[] GetAllSessionsWithStatus(SessionStatus sessionStatus);
    }
}