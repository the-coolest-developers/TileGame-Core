using System;
using System.Collections.Generic;
using WebApiBaseLibrary.DataAccess.Entities;

namespace TileGameServer.InSession.Domain.Entities
{
    public class GameSession : BaseEntity
    {
        public ITileField TileField { get; }

        public GameSession(ITileField tileField)
        {
            TileField = tileField;
        }

        private readonly Lazy<List<SessionPlayer>> _sessionPlayers = new();
        public IList<SessionPlayer> Players => _sessionPlayers.Value;
    }
}