using System;
using TileGameServer.DataAccess.Entities.Base;
using TileGameServer.DataAccess.Enums;

namespace TileGameServer.DataAccess.Entities
{
    public class GameSession : BaseEntity
    {
        public GameSessionStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}