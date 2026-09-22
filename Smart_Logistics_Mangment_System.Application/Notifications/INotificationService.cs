using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Notifications
{
    public interface INotificationService
    {
        Task SendToUserAsync(
            int userId,
            object data);
    }
}