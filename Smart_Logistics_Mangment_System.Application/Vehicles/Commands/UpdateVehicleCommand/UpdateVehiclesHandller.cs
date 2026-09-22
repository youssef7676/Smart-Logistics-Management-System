using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.Vehicles.DTOs;
using Smart_Logistics_Mangment_System.Application.Warehouses.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Vehicles.Commands.UpdateVehicleCommand
{
    public class UpdateVehiclesHandller(IVehicleReposatory verepo, IMapper mapper)
        : IRequestHandler<UpdateVehiclesCommand, VehicleDTO>
    {
        public async Task<VehicleDTO> Handle(UpdateVehiclesCommand request, CancellationToken cancellationToken)
        {
            Vehicle vehicles = await verepo.GetById(request.Id);

            if (vehicles == null)
                return null;

            vehicles.PlateNumber = request.PlateNumber;
            vehicles.Status = request.Status;
            vehicles.Capacity = request.Capacity;
            vehicles.Model = request.Model;





            verepo.Update(vehicles);
            await verepo.Save();
            return mapper.Map<VehicleDTO>(vehicles);
        }
    }
}
