using System;
using System.Collections.Generic;

namespace TileGameServer.BaseLibrary.Domain.Entities
{
    public class GameSession : BaseEntity
    {
        public GameSessionStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Guid> Players { get; set; }
        public int Capacity { get; set; }
    }
}