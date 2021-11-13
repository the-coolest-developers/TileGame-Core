using System;
using System.Collections.Generic;
using TileGameServer.BaseLibrary.Domain.Enums;
using WebApiBaseLibrary.DataAccess.Entities;

namespace TileGameServer.DataAccess.Entities
{
    public class GameSession : BaseEntity
    {
        public Guid CreatorId { get; set; }

        public GameSessionStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public int Capacity { get; set; }

        public ICollection<SessionPlayer> Players { get; set; }
    }
}