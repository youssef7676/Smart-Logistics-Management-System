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

namespace Smart_Logistics_Mangment_System.Application.ShipmentTrakings.Queries.GetAllShipmentTracking
{
    public class GetAllShipmentTrackingHandller(IShipmentTrackingReposatory trackrepo, IMapper mapper)
        : IRequestHandler<GetAllShipmentTrackingQueries, List<ShipmentTrackingDTO>>
    {
        public async Task<List<ShipmentTrackingDTO>> Handle(GetAllShipmentTrackingQueries request, CancellationToken cancellationToken)
        {
            List<ShipmentTracking> tracks = await trackrepo.GetAllTracking();

            List<ShipmentTrackingDTO> trackdto = mapper.Map<List<ShipmentTrackingDTO>>(tracks);
            return trackdto;
        }
    }
}
