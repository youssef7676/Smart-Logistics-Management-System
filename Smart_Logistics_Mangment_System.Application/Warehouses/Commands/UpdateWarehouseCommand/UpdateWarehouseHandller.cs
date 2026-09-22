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

namespace Smart_Logistics_Mangment_System.Application.Warehouses.Commands.UpdateWarehouseCommand
{
    public class UpdateWarehouseHandller(IWarehouseReposatory warerepo, IMapper mapper)
        : IRequestHandler<UpdateWarehouseCommands, DriversDTO>
    {
        public async Task<DriversDTO> Handle(UpdateWarehouseCommands request, CancellationToken cancellationToken)
        {
            Warehouse ware = await warerepo.GetById(request.Id);

            if (ware == null)
                return null;

            ware.Name = request.Name;
            ware.Location = request.Location;
            ware.Capacity = request.Capacity;


            warerepo.Update(ware);
            await warerepo.Save();
            return mapper.Map<DriversDTO>(ware);
        }
    }
}
