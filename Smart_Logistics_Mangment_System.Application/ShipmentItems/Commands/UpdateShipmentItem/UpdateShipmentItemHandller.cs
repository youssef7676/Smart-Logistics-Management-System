using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.Commands.CreateShipmentItem;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentItems.Commands.UpdateShipmentItem
{
    public class UpdateShipmentItemHandller(IShipmentItemReposatory itemrepo, IMapper mapper)
        : IRequestHandler<UpdateShipmentItemCommand, ShipmentItemDTO>
    {
        public async Task<ShipmentItemDTO> Handle(UpdateShipmentItemCommand request, CancellationToken cancellationToken)
        {


            {
                var item = await itemrepo.GetById(request.Id);

                if (item == null || item.IsDeleted)
                {
                    throw new KeyNotFoundException(
                        "Shipment Item not found.");
                }

                if (item.ShipmentId.HasValue)
                {
                    throw new InvalidOperationException(
                        "Cannot update item after shipment creation.");
                }

                item.ItemName = request.ItemName;
                item.Quantity = request.Quantity;
                item.Weight = request.Weight;
                item.Description = request.Description;

                itemrepo.Update(item);

                await itemrepo.Save();

                return mapper.Map<ShipmentItemDTO>(item);
            }
        }
    }
}
