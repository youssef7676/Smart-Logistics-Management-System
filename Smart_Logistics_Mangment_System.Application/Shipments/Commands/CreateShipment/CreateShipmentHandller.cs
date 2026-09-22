using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Commands.CreateShipment
{
    public class CreateShipmentHandller(IShipmentRequestItemReposatory reqrepo,IShipmentReposatory shiprepo,IMapper mapper)
    : IRequestHandler<CreateShipmentCommand, ShipmentDTO>
    {
        public async Task<ShipmentDTO> Handle(CreateShipmentCommand request,CancellationToken cancellationToken)
        {
            var shipmentRequest = await reqrepo.GetByIdWithDetails( request.ShipmentRequestId);

            if (shipmentRequest == null)
            {
                throw new KeyNotFoundException("Shipment Request not found.");
            }

            if (shipmentRequest.IsDeleted)
            {
                throw new InvalidOperationException("Shipment Request is deleted.");
            }

            // Shipment already created
            if (shipmentRequest.ShipmentId.HasValue)
            {
                throw new InvalidOperationException("Shipment already created for this request.");
            }

            // Request must be approved
            if (shipmentRequest.Status != ShipmentRequestStatus.Approved)
            {
                throw new InvalidOperationException("Shipment Request must be approved first.");
            }

            var shipment = new Shipment
            {
                ShipmentRequestId = shipmentRequest.Id,

                CustomerId = shipmentRequest.CustomerId,

                PickupWarehouseId = shipmentRequest.PickupWarehouseId,

                DestinationAddress = shipmentRequest.DestinationAddress,
                DestinationLatitude = request.DestinationLatitude,

                DestinationLongitude =request.DestinationLongitude,

                TotalWeight =shipmentRequest.Items.Sum(x => x.Weight * x.Quantity),

                Status = ShipmentStatus.Created,

                Priority = request.Priority
            };

            await shiprepo.Add(shipment);

            await shiprepo.Save();

            // Link Shipment to ShipmentRequest
            shipmentRequest.ShipmentId = shipment.Id;

            reqrepo.Update(shipmentRequest);

            await reqrepo.Save();

            var createdShipment =await shiprepo.GetShipmentByIdWithDetails(shipment.Id);

            if (createdShipment == null)
            {
                throw new KeyNotFoundException("Shipment was created but could not be retrieved.");
            }

            return mapper.Map<ShipmentDTO>(createdShipment);
        }
    }
}



