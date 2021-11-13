using WebApiBaseLibrary.DataAccess.Entities;

namespace TileGameServer.DataAccess.Entities
{
    public class Player : BaseEntity
    {
        public string Nickname { get; set; }
    }
}