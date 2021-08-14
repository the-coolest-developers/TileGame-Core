using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileGameServer.BaseLibrary.Domain.Entities;

namespace TileGameServer.Domain.Models.Responses
{
    public class ListCreatedGameSessionsResponse
    {
        public GameSession[] GameSessions { get; set; }
    }
}
