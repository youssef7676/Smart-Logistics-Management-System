using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Smart_Logistics_Mangment_System.Application.Warehouses.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Warehouses.Queries.GetAllWarehouse
{
    public class GetAllWareHouseHandller(IWarehouseReposatory warerepo, IMapper mapper)
        : IRequestHandler<GetAllWareHouseQueries, List<DriversDTO>>
    {
        public async Task<List<DriversDTO>> Handle(GetAllWareHouseQueries request, CancellationToken cancellationToken)
        {
            List<Warehouse> Ware = await warerepo.GetAll();
            List<DriversDTO> waredto = mapper.Map<List<DriversDTO>>(Ware);
            return waredto;
        }
    }
}
