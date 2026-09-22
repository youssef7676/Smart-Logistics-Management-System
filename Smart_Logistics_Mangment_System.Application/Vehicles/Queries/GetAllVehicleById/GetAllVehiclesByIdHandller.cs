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

namespace Smart_Logistics_Mangment_System.Application.Vehicles.Queries.GetAllVehicleById
{
    public class GetAllVehiclesByIdHandller(IVehicleReposatory verepo, IMapper mapper)
        : IRequestHandler<GetAllVehiclesByIdQueries, VehicleDTO>
    {
        public async Task<VehicleDTO> Handle(GetAllVehiclesByIdQueries request, CancellationToken cancellationToken)
        {
            Vehicle vehicles = await verepo.GetById(request.Id);

            if (vehicles == null)
                return null;

            return mapper.Map<VehicleDTO>(vehicles);
        }
    }
}
