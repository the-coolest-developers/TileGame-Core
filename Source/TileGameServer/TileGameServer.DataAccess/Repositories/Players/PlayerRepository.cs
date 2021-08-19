using TileGameServer.BaseLibrary.Domain.Entities;
using TileGameServer.DataAccess.Context;
using WebApiBaseLibrary.DataAccess.Repositories;

namespace TileGameServer.DataAccess.Repositories.Players
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