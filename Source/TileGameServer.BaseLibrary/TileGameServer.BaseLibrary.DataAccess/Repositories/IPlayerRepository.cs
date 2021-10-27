using TileGameServer.BaseLibrary.DataAccess.Entities;
using WebApiBaseLibrary.DataAccess.Repositories;

namespace TileGameServer.BaseLibrary.DataAccess.Repositories
{
    public interface IPlayerRepository : IRepository<Player>, IDatabaseRepository
    {
    }
}