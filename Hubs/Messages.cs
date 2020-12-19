using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace Bingo.Hubs
{
    public class Messages : Hub
    {
        public override Task OnConnectedAsync()
        {
            //this.Context.ConnectionId;
            return base.OnConnectedAsync();
        }
        public Task NotifyAll(string message){
            return Clients.Client("").SendAsync("recibeMessage", message);
        }
    }
}