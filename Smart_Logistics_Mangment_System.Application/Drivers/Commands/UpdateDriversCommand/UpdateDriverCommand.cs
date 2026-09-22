using MediatR;
using Smart_Logistics_Mangment_System.Application.Drivers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Drivers.Commands.UpdateDriversCommand
{
    public class UpdateDriverCommand :IRequest<DriversDTO>
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string LicenseNumber { get; set; }

    }
}
