using Microsoft.AspNetCore.SignalR;

namespace RealEstate.API.Hubs
{
    public class PropertyHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
