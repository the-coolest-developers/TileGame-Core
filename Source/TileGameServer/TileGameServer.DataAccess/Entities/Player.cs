using System;

namespace TileGameServer.DataAccess.Entities
{
    public class Player
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
    }
}