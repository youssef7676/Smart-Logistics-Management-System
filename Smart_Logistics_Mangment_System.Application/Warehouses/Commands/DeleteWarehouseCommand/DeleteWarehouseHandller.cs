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

namespace Smart_Logistics_Mangment_System.Application.Warehouses.Commands.DeleteWarehouseCommand
{
    public class DeleteWarehouseHandller(IWarehouseReposatory warerepo, IMapper mapper)
        : IRequestHandler<DeleteWarehouseCommands, DriversDTO>
    {
        public async Task<DriversDTO> Handle(DeleteWarehouseCommands request, CancellationToken cancellationToken)
        {
            Warehouse Deleteware = await warerepo.GetById(request.Id);

            if (Deleteware == null)
                return null;


            warerepo.Delete(Deleteware.Id);
            return mapper.Map<DriversDTO>(Deleteware);
        }
    }
}
