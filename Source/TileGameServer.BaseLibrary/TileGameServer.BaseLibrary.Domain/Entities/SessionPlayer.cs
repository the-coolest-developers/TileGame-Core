using System;
using WebApiBaseLibrary.DataAccess.Entities;

namespace TileGameServer.BaseLibrary.Domain.Entities
{
    public class SessionPlayer : BaseEntity
    {
        public Guid GameSessionId { get; set; }
        public GameSession GameSession { get; set; }
    }
}