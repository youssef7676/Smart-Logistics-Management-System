using MediatR;
using Smart_Logistics_Mangment_System.Application.Vehicles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Vehicles.Commands.DeleteVehicleCommand
{
    public class DeleteVehicleCommand : IRequest<VehicleDTO>
    {
        public int Id { get; set; }
    }
}
