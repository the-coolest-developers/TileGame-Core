using WebApiBaseLibrary.DataAccess.Entities;

namespace TileGameServer.InSession.Domain.Entities
{
    public class SessionPlayer : BaseEntity
    {
        public string Nickname { get; set; }
        public GameSession GameSession { get; init; }
    }
}