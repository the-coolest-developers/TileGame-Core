using System;
using System.Threading.Tasks;
using TileGameServer.DataAccess.Entities;
using TileGameServer.DataAccess.Enums;
using TileGameServer.DataAccess.Repositories.Generic;

namespace TileGameServer.DataAccess.Repositories
{
    public interface IListGameSessionsRepository : IRepository<GameSession>
    {
    }
}