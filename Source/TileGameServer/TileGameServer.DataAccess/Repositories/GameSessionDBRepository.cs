using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileGameServer.BaseLibrary.Domain.Entities;
using WebApiBaseLibrary.DataAccess.Repositories;

namespace TileGameServer.DataAccess.Repositories
{
    class GameSessionDBRepository : EntityFrameworkBaseRepository<GameSession>
    {
        public GameSessionDBRepository(DbContext entityContext) : base(entityContext)
        { }
    }
}
