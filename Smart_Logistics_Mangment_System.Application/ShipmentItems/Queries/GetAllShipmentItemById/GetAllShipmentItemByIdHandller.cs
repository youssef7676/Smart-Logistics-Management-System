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

namespace Smart_Logistics_Mangment_System.Application.ShipmentItems.Queries.GetAllShipmentItemById
{
    public class GetAllShipmentItemByIdHandller(IShipmentItemReposatory itemrepo, IMapper mapper)
        : IRequestHandler<GetAllShipmentItemByIdQueries, ShipmentItemDTO>
    {
        public async Task<ShipmentItemDTO> Handle(GetAllShipmentItemByIdQueries request, CancellationToken cancellationToken)
        {
            ShipmentItem items = await itemrepo.GetById(request.Id);

            if (items == null)
                return null;

            return mapper.Map<ShipmentItemDTO>(items);
        }
    }
}
