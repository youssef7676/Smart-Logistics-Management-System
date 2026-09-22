using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentItems.Commands.CreateShipmentItem
{
    public class CreateShipmentItemHandller(IShipmentItemReposatory itemrepo,IShipmentRequestItemReposatory reqrepo,IMapper mapper)
        : IRequestHandler<CreateShipmentItemCommand, ShipmentItemDTO>
    {
        public async Task<ShipmentItemDTO> Handle(CreateShipmentItemCommand request, CancellationToken cancellationToken)
        {

                var shipmentRequest =
                    await reqrepo.GetById(request.ShipmentRequestId);

                if (shipmentRequest == null || shipmentRequest.IsDeleted)
                {
                    throw new KeyNotFoundException(
                        "Shipment Request not found.");
                }

                var item = new ShipmentItem
                {
                    ItemName = request.ItemName,
                    Quantity = request.Quantity,
                    Weight = request.Weight,
                    Description = request.Description,
                    ShipmentRequestId = request.ShipmentRequestId
                };

                await itemrepo.Add(item);

                await itemrepo.Save();

                return mapper.Map<ShipmentItemDTO>(item);
            }
        }
}