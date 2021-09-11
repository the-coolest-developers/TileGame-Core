using WebApiBaseLibrary.DataAccess.Entities;

namespace TileGameServer.BaseLibrary.Domain.Entities
{
    public class Player : BaseEntity
    {
        public string Nickname { get; set; }
    }
}