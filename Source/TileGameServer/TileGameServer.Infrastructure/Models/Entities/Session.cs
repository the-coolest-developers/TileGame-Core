using TileGameServer.Enums;
using TileGameServer.Infrastructure.Models.Entities.Base;

namespace TileGameServer.Infrastructure.Models.Entities
{
    public class Session : BaseEntity
    {
        public SessionStatus Status { get; set; }
    }
}