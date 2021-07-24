using System;
using TileGameServer.DataAccess.Enums;
using System.Collections.Generic;
using WebApiBaseLibrary.DataAccess.Entities;

namespace TileGameServer.DataAccess.Entities
{
    public class GameSession : BaseEntity
    {
        public GameSessionStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Guid> PlayerIds { get; set; } = new List<Guid>();
        public int Capacity { get; set; }
    }
}