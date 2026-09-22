using MediatR;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Drivers.Commands.CreateDriversCommand
{
    public class CreateDriverCommand : IRequest<int>
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string LicenseNumber { get; set; }
    }
}
