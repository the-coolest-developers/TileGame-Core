using TileGameServer.BaseLibrary.DataAccess.Context;
using TileGameServer.BaseLibrary.DataAccess.Entities;
using WebApiBaseLibrary.DataAccess.Repositories;

namespace TileGameServer.BaseLibrary.DataAccess.Repositories
{
    public class PlayerRepository : EntityFrameworkBaseRepository<Player>, IPlayerRepository
    {
        private readonly PlayerContext _playerContext;

        public PlayerRepository(PlayerContext playerContext) : base(playerContext)
        {
            _playerContext = playerContext;
        }
    }
}