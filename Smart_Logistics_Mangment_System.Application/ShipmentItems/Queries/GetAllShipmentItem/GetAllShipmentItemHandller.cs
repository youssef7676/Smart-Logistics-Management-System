using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using Smart_Logistics_Mangment_System.Application.Vehicles.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentItems.Queries.GetAllShipmentItem
{
    public class GetAllShipmentItemHandller(IShipmentItemReposatory itemrepo, IMapper mapper)
        : IRequestHandler<GetAllShipmentItemQueries, List<ShipmentItemDTO>>
    {
        public async Task<List<ShipmentItemDTO>> Handle(GetAllShipmentItemQueries request, CancellationToken cancellationToken)
        {

            List<ShipmentItem> items = await itemrepo.GetAllShipmentItems();
            List<ShipmentItemDTO> itemdto = mapper.Map<List<ShipmentItemDTO>>(items);
            return itemdto;
        }
    }
}
