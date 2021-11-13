using System;
using System.Collections.Generic;
using WebApiBaseLibrary.DataAccess.Entities;

namespace TileGameServer.InSession.DataAccess.Entities
{
    public class GameSession : BaseEntity
    {
        private readonly Lazy<ICollection<SessionPlayer>> _sessionPlayers;
        public ICollection<SessionPlayer> Players => _sessionPlayers.Value;
    }
}
