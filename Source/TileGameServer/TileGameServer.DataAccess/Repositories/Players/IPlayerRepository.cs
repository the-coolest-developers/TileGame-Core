using TileGameServer.BaseLibrary.Domain.Entities;
using WebApiBaseLibrary.DataAccess.Repositories;

namespace TileGameServer.DataAccess.Repositories.Players
{
    public interface IPlayerRepository : IRepository<Player>
    {
    }
}