using MediatR;
using Smart_Logistics_Mangment_System.Application.Vehicles.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Vehicles.Commands.UpdateVehicleCommand
{
    public class UpdateVehiclesCommand : IRequest<VehicleDTO>

    {
        public int Id { get; set; }

        public string PlateNumber { get; set; }

        public string Model { get; set; }

        public double Capacity { get; set; }

        public VehicleStatus Status { get; set; }

        public int? DriverId { get; set; }

        public string? DriverName { get; set; }
    }
}
