using System;
using TileGameServer.DataAccess.Entities.Base;
using TileGameServer.DataAccess.Enums;

namespace TileGameServer.DataAccess.Entities
{
    public class AccountInSession : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid GameSessionId { get; set; }
    }
}