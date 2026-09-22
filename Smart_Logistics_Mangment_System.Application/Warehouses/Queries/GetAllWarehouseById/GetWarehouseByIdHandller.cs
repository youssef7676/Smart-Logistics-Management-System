using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.Warehouses.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Warehouses.Queries.GetAllWarehouseById
{
    public class GetWarehouseByIdHandller(IWarehouseReposatory warerepo, IMapper mapper)
        : IRequestHandler<GetWarehouseByIdQueries, DriversDTO>
    {
        public async Task<DriversDTO> Handle(GetWarehouseByIdQueries request, CancellationToken cancellationToken)
        {
            Warehouse ware = await warerepo.GetById(request.Id);

            if (ware == null)
                return null;

            return mapper.Map<DriversDTO>(ware);
        }
    }
}
