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

namespace Smart_Logistics_Mangment_System.Application.Vehicles.Commands.DeleteVehicleCommand
{
    public class DeleteVehicleHandller(IVehicleReposatory verepo, IMapper mapper)
        : IRequestHandler<DeleteVehicleCommand, VehicleDTO>
    {
        public async Task<VehicleDTO> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            Vehicle Deletevehicle = await verepo.GetById(request.Id);

            if (Deletevehicle == null)
                return null;


            verepo.Delete(Deletevehicle.Id);
            return mapper.Map<VehicleDTO>(Deletevehicle);
        }
    }
}
