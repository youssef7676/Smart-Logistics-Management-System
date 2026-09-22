using Microsoft.AspNetCore.SignalR;
using Smart_Logistics_Mangment_System.API.Hubs;
using Smart_Logistics_Mangment_System.Application.Notifications;

namespace Smart_Logistics_Mangment_System.API.Services
{
    public class NotificationService(
        IHubContext<NotificationHub> hubContext)
        : INotificationService
    {
        public async Task SendToUserAsync(
            int userId,
            object data)
        {
            await hubContext.Clients
                .Group($"User_{userId}")
                .SendAsync(
                    "ReceiveNotification",
                    data);
        }
    }
}