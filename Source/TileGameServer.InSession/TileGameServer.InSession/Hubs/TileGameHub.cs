using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TileGameServer.InSession.DataAccess.Context;
using TileGameServer.InSession.Domain.Entities;
using TileGameServer.InSession.Domain.Enums;
using TileGameServer.InSession.Domain.Library;

namespace TileGameServer.InSession.Hubs
{
    public class TileGameHub : Hub
    {
        private readonly IInSessionContext _inSessionContext;
        private readonly ITileLibrary _tileLibrary;

        public TileGameHub(
            IInSessionContext inSessionContext,
            ITileLibrary tileLibrary)
        {
            _inSessionContext = inSessionContext;

            _tileLibrary = tileLibrary;
        }

        public override Task OnConnectedAsync()
        {
            //For testing purposes
            /*var tileField = new TileField(
                new Tile(),
                new TilePosition { X = 50, Y = 50 },
                new FieldSize { Height = 100, Width = 100 });

            var gameSession = new GameSession(tileField)
            {
                Id = new Guid("231d304e-fb83-4738-8507-30807ad400b8")
            };
            var player = new SessionPlayer
            {
                Id = new Guid("321d304e-fb83-4738-8507-30807ad400a7"),
                Nickname = "Nickname!",
                GameSession = gameSession
            };
            gameSession.Players.Add(player);
            _inSessionContext.EntitySet<SessionPlayer>().Add(player);
            _inSessionContext.EntitySet<GameSession>().Add(gameSession);*/

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task Connect(Guid playerId)
        {
            if (playerId == default)
            {
                return;
            }

            var sessionPlayer = _inSessionContext.EntitySet<SessionPlayer>()
                .FirstOrDefault(p => p.Id == playerId);

            if (sessionPlayer != null)
            {
                await Clients.All.SendAsync("PlayerConnected", sessionPlayer.Nickname);
            }
        }

        public async Task Disconnect(Guid playerId)
        {
            var players = _inSessionContext.EntitySet<SessionPlayer>();
            var sessionPlayer = players.FirstOrDefault(p => p.Id == playerId);

            if (sessionPlayer == null)
            {
                return;
            }

            players.Remove(sessionPlayer);

            var gameSession = _inSessionContext.EntitySet<GameSession>()
                .FirstOrDefault(gs => gs.Players.Any(p => p.Id == playerId));
            if (gameSession != null)
            {
                var playerInSession = gameSession.Players.FirstOrDefault(p => p.Id == playerId);

                gameSession.Players.Remove(playerInSession);
            }

            await Clients.All.SendAsync("PlayerDisconnected", sessionPlayer.Nickname);
        }

        public async Task PlaceTile(
            Guid playerId,
            int tileTypeId,
            int x,
            int y,
            TileRotation tileRotation)
        {
            var players = _inSessionContext.EntitySet<SessionPlayer>();
            var sessionPlayer = players.FirstOrDefault(p => p.Id == playerId);

            if (sessionPlayer == null)
            {
                return;
            }

            var tileField = sessionPlayer.GameSession.TileField;

            var tile = _tileLibrary.GetTile(tileTypeId, tileRotation);

            var position = new TilePosition
            {
                X = x,
                Y = y
            };

            tileField.PlaceTile(tile, position);

            await Clients.All.SendAsync("PlacedTileNotification",
                sessionPlayer.Nickname,
                x,
                y,
                tileTypeId,
                tileRotation);
        }
    }
}