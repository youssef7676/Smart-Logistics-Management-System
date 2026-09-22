using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Application.Vehicles.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Queries.GetAllShipmentsById
{
    public class GetAllShipmentByIdHandller(IShipmentReposatory shiprepo, IMapper mapper)
        : IRequestHandler<GetAllShipmentByIdQueries, ShipmentDTO>
    {
        public async Task<ShipmentDTO> Handle(GetAllShipmentByIdQueries request, CancellationToken cancellationToken)
        {
            Shipment shipments = await shiprepo.GetShipmentByIdWithDetails(request.Id);

            if (shipments == null)
                return null;

            return mapper.Map<ShipmentDTO>(shipments);
        }
    }
}
