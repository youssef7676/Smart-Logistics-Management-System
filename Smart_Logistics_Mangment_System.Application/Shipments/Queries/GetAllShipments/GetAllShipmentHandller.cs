using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Application.Vehicles.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Queries.GetAllShipments
{
    public class GetAllShipmentHandller(IShipmentReposatory shiprepo, IMapper mapper)
        : IRequestHandler<GetAllShipmentQueries, List<ShipmentDTO>>
    {
        public async Task<List<ShipmentDTO>> Handle(GetAllShipmentQueries request, CancellationToken cancellationToken)
        {
            List<Shipment> shipments = await shiprepo.GetAllShipments( request.PageNumber, request.PageSize);

            List<ShipmentDTO> shipdto = mapper.Map<List<ShipmentDTO>>(shipments);
            return shipdto;
        }
    }
}
