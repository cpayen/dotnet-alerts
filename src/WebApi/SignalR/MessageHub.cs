using Microsoft.AspNetCore.SignalR;
using WebApi.Entities;

namespace WebApi.SignalR;

public class MessageHub : Hub
{
    public async Task SendAlertsync(Alert alert)
    {
        await Clients.All.SendAsync("ReceiveAlert", alert);
    }
}