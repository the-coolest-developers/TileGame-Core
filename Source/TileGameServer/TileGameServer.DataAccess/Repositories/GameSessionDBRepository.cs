using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileGameServer.BaseLibrary.DataAccess.Context;
using TileGameServer.BaseLibrary.Domain.Entities;
using WebApiBaseLibrary.DataAccess.Repositories;

namespace TileGameServer.DataAccess.Repositories
{
    public class GameSessionDBRepository : EntityFrameworkBaseRepository<GameSession>, IGameSessionRepository
    {
        public GameSessionDBRepository(GameSessionContext entityContext) : base(entityContext)
        { }

        public bool ExistsWithPlayer(Guid playerId)
        {
            var player = EntityDbSet.Find(playerId);
            
            if(player != null) 
            {
                return true;
            }

            return false;
        }

        public Task<bool> ExistsWithPlayerAsync(Guid playerId)
        {
            var res = ExistsWithPlayer(playerId);
            return Task.FromResult(res);
        }

        public Task<GameSession> GetWithPlayerAsync(Guid playerId)
        {
            var player = Get(playerId);
            return Task.FromResult(player);
        }
    }
}
