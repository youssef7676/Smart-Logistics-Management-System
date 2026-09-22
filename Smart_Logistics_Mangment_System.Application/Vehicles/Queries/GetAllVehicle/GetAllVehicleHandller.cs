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

namespace Smart_Logistics_Mangment_System.Application.Vehicles.Queries.GetAllVehicle
{
    public class GetAllVehicleHandller(IVehicleReposatory verepo, IMapper mapper)
        : IRequestHandler<GetAllVehicleQueries, List<VehicleDTO>>
    {
        public async Task<List<VehicleDTO>> Handle(GetAllVehicleQueries request, CancellationToken cancellationToken)
        {
            List<Vehicle> vehicles = await verepo.GetAll();
            List<VehicleDTO> vedto = mapper.Map<List<VehicleDTO>>(vehicles);
            return vedto;
        }
    }
}
