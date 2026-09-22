using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Vehicles.Commands.CreateVehicleCommand
{
    public class CreateVehiclesHandller(IVehicleReposatory verepo, IMapper mapper)
        : IRequestHandler<CreateVehiclesCommands, int>
    {
        public async Task<int> Handle(CreateVehiclesCommands request, CancellationToken cancellationToken)
        {
            Vehicle newvehicle = mapper.Map<Vehicle>(request);
            await verepo.Add(newvehicle);
            await verepo.Save();
            return newvehicle.Id;
        }
    }
}
