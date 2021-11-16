using WebApiBaseLibrary.DataAccess.Entities;

namespace TileGameServer.InSession.DataAccess.Entities
{
    public class SessionPlayer : BaseEntity
    {
        public string Nickname { get; set; }
        public string IsAuthorized { get; set; }
    }
}