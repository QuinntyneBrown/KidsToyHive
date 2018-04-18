using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace KidsToyHive.SPA.Hubs
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AppHub: Hub
    {
        public Task Send(string message) => Clients.All.SendAsync("Send", message);
    }
}
