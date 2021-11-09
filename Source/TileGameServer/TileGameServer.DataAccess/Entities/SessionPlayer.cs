using System;
using WebApiBaseLibrary.DataAccess.Entities;

namespace TileGameServer.DataAccess.Entities
{
    public class SessionPlayer : BaseEntity
    {
        public Guid GameSessionId { get; set; }
        
        public GameSession GameSession { get; set; }
    }
}