using Microsoft.AspNetCore.Identity;
using Smart_Logistics_Mangment_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Domain.Models
{
    public class AppUser : BaseEntity
    {
        public string FullName { get; set; }

        public UserRole Role { get; set; }
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }

        public Customer Customers { get; set; }

        public Driver Drivers { get; set; }

        public Employee Employees { get; set; }
    }
}
