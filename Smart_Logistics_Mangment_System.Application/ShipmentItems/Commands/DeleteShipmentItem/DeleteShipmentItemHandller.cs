using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentItems.Commands.DeleteShipmentItem
{
    public class DeleteShipmentItemHandller(IShipmentItemReposatory itemrepo, IMapper mapper)
        : IRequestHandler<DeleteShipmentItemCommand, ShipmentItemDTO>
    {
        public async Task<ShipmentItemDTO> Handle(DeleteShipmentItemCommand request, CancellationToken cancellationToken)
        {
            ShipmentItem items = await itemrepo.GetById(request.Id);

            if (items == null)
                return null;


            itemrepo.Delete(items.Id);
            return mapper.Map<ShipmentItemDTO>(items);
        }
    }
}
