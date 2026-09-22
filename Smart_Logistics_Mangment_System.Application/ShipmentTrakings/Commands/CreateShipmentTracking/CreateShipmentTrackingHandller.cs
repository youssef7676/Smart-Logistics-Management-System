using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentTrakings.Commands.CreateShipmentTracking
{
    public class CreateShipmentTrackingHandller(IShipmentTrackingReposatory trackrepo, IMapper mapper,IShipmentReposatory shiprepo)
        : IRequestHandler<CreateShipmentTrackingCommand, ShipmentTrackingDTO>
    {
        public async
            Task<ShipmentTrackingDTO> Handle(CreateShipmentTrackingCommand request, CancellationToken cancellationToken)
        {
                var shipment = await shiprepo.GetById(request.ShipmentId);

                if (shipment == null || shipment.IsDeleted)
                {
                    throw new KeyNotFoundException("Shipment not found.");
                }

                var tracking = new ShipmentTracking
                {
                    ShipmentId = request.ShipmentId,
                    Status = request.Status,
                    Location = request.Location,
                    Note = request.Note
                };

                await trackrepo.Add(tracking);

                await trackrepo.Save();

                return mapper.Map<ShipmentTrackingDTO>(tracking);
        }
    }
}
