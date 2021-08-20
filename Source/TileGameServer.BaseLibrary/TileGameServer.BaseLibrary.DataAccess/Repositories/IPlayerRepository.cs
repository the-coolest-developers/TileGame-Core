using TileGameServer.BaseLibrary.Domain.Entities;
using WebApiBaseLibrary.DataAccess.Repositories;

namespace TileGameServer.BaseLibrary.DataAccess.Repositories
{
    public interface IPlayerRepository : IRepository<Player>
    {
    }
}