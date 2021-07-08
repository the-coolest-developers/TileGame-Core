using System;
using TileGameServer.DataAccess.Entities.Base;
using TileGameServer.DataAccess.Enums;
using System.Collections.Generic;
using TileGameServer.Infrastructure.Models;

namespace TileGameServer.DataAccess.Entities
{
    public class GameSession : BaseEntity
    {
        public GameSessionStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Guid> PlayerIds { get; set; } = new List<Guid>();
    }
}
