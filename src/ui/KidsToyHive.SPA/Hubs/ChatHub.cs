using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace KidsToyHive.SPA.Hubs
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChatHub: Hub
    {
        public Task Send(string message) => Clients.All.SendAsync("Send", message);
    }
}
