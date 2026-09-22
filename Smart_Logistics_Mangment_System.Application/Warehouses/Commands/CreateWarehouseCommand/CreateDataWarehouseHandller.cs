using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Warehouses.Commands.CreateWarehouseCommand
{
    public class CreateDataWarehouseHandller(IWarehouseReposatory warerepo, IMapper mapper)
        : IRequestHandler<CreateDataWarehouseCommand, int>
    {
        public async Task<int> Handle(CreateDataWarehouseCommand request, CancellationToken cancellationToken)
        {
            Warehouse newware = mapper.Map<Warehouse>(request);
            await warerepo.Add(newware);
            await warerepo.Save();
            return newware.Id;
        }
    }
}
