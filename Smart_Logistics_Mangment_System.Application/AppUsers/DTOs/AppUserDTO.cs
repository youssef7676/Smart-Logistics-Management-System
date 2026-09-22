using Smart_Logistics_Mangment_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.AppUsers.DTOs
{
    public class AppUserDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
