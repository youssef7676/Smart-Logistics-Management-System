using MediatR;
using Smart_Logistics_Mangment_System.Application.AppUsers.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.AppUsers.Commands.Register
{
    public class RegisterCommand : IRequest<int>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string CompanyName { get; set; }
        public string Address { get; set; }
    }
}
