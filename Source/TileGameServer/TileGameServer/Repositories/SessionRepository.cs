using System;
using System.Collections.Generic;
using TileGameServer.Enums;
using TileGameServer.Hubs;
using TileGameServer.Models.Entities;

namespace TileGameServer.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        //private static List<Session> ActiveSessions { get; }
        
        //private MenuHub MenuHub { get; }

        public SessionRepository(/*MenuHub menuHub*/)
        {
            //MenuHub = menuHub;
        }

        public bool SessionExists(Guid gameId)
        {
            //var g = MenuHub.Clients.Group(gameId.ToString());

            return true;
        }

        public bool UserIsInSession(Guid userId)
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

        public Session[] GetSessionWithId(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public void CloseSession(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public void AddUserToSession(Guid usedId)
        {
            //MenuHub.Groups.AddToGroupAsync();
        }
    }
}