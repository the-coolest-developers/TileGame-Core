using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TileGameServer.InSession.DataAccess.Context;

namespace TileGameServer.InSession.Hubs
{
    public class TileGameHub : Hub
    {
        private readonly IInSessionContext _inSessionContext;

        public TileGameHub(IInSessionContext inSessionContext)
        {
            _inSessionContext = inSessionContext;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public Task Authorize(string user, string message)
        {
            return Task.CompletedTask;
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}