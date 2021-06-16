using System;
using TileGameServer.Infrastructure.Enums;
using TileGameServer.Infrastructure.Models.Entities.Base;

namespace TileGameServer.Infrastructure.Models.Entities
{
    public class GameSession : BaseEntity
    {
        public GameSessionStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}