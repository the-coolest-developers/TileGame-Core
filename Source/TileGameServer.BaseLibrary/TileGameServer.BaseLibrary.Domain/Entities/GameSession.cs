using System;
using System.Collections.Generic;
using TileGameServer.BaseLibrary.Domain.Enums;
using WebApiBaseLibrary.DataAccess.Entities;

namespace TileGameServer.BaseLibrary.Domain.Entities
{
    public class GameSession : BaseEntity
    {
        public GameSessionStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public int Capacity { get; set; }

        public List<Player> Players { get; set; }
    }
}