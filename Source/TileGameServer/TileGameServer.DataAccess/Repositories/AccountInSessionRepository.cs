using System;
using System.Linq;
using System.Threading.Tasks;
using TileGameServer.DataAccess.Context;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.Enums;
using TileGameServer.DataAccess.Repositories.Generic;

namespace TileGameServer.DataAccess.Repositories
{
    public class GameGameSessionRepository : EntityFrameworkBaseRepository<AccountsInSession>, IAccountInSessionRepository
    {
    }
}