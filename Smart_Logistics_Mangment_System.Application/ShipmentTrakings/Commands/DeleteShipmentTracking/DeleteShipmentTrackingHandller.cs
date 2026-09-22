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

namespace Smart_Logistics_Mangment_System.Application.ShipmentTrakings.Commands.DeleteShipmentTracking
{
    public class DeleteShipmentTrackingHandller(IShipmentTrackingReposatory trackrepo, IMapper mapper)
        : IRequestHandler<DeleteShipmentTrackingCommand, ShipmentTrackingDTO>
    {
        public async Task<ShipmentTrackingDTO> Handle(DeleteShipmentTrackingCommand request, CancellationToken cancellationToken)
        {
            ShipmentTracking trakcs = await trackrepo.GetById(request.Id);

            if (trakcs == null)
                return null;


            trackrepo.Delete(trakcs.Id);
            return mapper.Map<ShipmentTrackingDTO>(trakcs);
        }
    }
}
