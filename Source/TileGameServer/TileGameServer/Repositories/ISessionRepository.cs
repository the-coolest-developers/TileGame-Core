using System;
using TileGameServer.Enums;
using TileGameServer.Models.Entities;

namespace TileGameServer.Repositories
{
    public interface ISessionRepository
    {
        bool SessionExists(Guid gameId);
        bool UserIsInSession(Guid userId);

        SessionStatus GetSessionStatus(Guid gameId);
        Session[] GetAllSessionsWithStatus(SessionStatus sessionStatus);
        Session[] GetSessionWithId(Guid gameId);

        void CloseSession(Guid gameId);
        void AddUserToSession(Guid usedId);
    }
}