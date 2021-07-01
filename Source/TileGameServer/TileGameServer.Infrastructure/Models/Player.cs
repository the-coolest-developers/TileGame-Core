using TileGameServer.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace TileGameServer.Infrastructure.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
    }
}
