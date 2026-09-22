using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentTrakings.Queries.GetAllShipmentTrackingById
{
    public class GetAllShipmentTrackingByIdHandller(IShipmentTrackingReposatory trackrepo, IMapper mapper)
        : IRequestHandler<GetAllShipmentTrackingByIdQueries, ShipmentTrackingDTO>
    {
        public async Task<ShipmentTrackingDTO> Handle(GetAllShipmentTrackingByIdQueries request, CancellationToken cancellationToken)
        {
            ShipmentTracking tracks = await trackrepo.GetById(request.Id);

            if (tracks == null)
                return null;

            return mapper.Map<ShipmentTrackingDTO>(tracks);
        }
    }
}
