using Microsoft.AspNetCore.SignalR;

namespace Intern.WebAPI.Hubs
{
    public class MyHub : Hub
    {
        public async override Task OnConnectedAsync()
        {
            //tüm connect olanlara mesj gönderiyor
            await Clients.All.SendAsync("Connected", Context.ConnectionId);
        }

        public async  Task SendMessage(string message)
        {
            try
            {
                await Clients.All.SendAsync("ReceiveMessage", message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task SendTag(string message)
        {
            try
            {
                await Clients.All.SendAsync("ReceiveTag", message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task SendVehicle(string message)
        {
            try
            {
                await Clients.All.SendAsync("ReceiveVehicle", message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}