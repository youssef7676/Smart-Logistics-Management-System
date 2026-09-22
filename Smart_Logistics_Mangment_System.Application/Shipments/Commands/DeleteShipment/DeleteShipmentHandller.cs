using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Commands.DeleteShipment
{
    public class DeleteShipmentHandller(IShipmentReposatory shiprepo, IMapper mapper)
        : IRequestHandler<DeleteShipmentCommand, ShipmentDTO>
    {
        public async Task<ShipmentDTO> Handle(DeleteShipmentCommand request, CancellationToken cancellationToken)
        {
            Shipment shipment = await shiprepo.GetById(request.Id);

            if (shipment == null)
                return null;


            shiprepo.Delete(shipment.Id);
            return mapper.Map<ShipmentDTO>(shipment);
        }
    }
}
