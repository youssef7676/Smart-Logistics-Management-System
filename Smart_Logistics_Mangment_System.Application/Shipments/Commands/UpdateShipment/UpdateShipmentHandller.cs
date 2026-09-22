using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Commands.UpdateShipment
{
    public class UpdateShipmentHandller(IShipmentReposatory shiprepo, IMapper mapper)
        : IRequestHandler<UpdateShipmentCommand, ShipmentDTO>
    {
        public async Task<ShipmentDTO> Handle(UpdateShipmentCommand request, CancellationToken cancellationToken)
        {

                var shipment =await shiprepo.GetShipmentByIdWithDetails(request.Id);

                if (shipment == null)
                {
                    throw new KeyNotFoundException("Shipment not found.");
                }

                shipment.DestinationAddress = request.DestinationAddress;
                shipment.Priority = request.Priority;

            shiprepo.Update(shipment);

                await shiprepo.Save();

                var updatedShipment = await shiprepo.GetShipmentByIdWithDetails(request.Id);

                return mapper.Map<ShipmentDTO>( updatedShipment);
            }
        }
}