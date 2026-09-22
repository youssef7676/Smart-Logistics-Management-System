using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentTrakings.Commands.UpdateShipmentTracking
{
    public class UpdateShipmentTrackingHandller(IShipmentTrackingReposatory trackrepo, IMapper mapper)
        : IRequestHandler<UpdateShipmentTrackingCommand, ShipmentTrackingDTO>
    {
        public async Task<ShipmentTrackingDTO> Handle(UpdateShipmentTrackingCommand request, CancellationToken cancellationToken)
        {
            var tracking = await trackrepo.GetById(request.Id);

            if (tracking == null || tracking.IsDeleted)
            {
                throw new KeyNotFoundException("Shipment Tracking not found.");
            }

            tracking.Status = request.Status;
            tracking.Location = request.Location;
            tracking.Note = request.Note;

            trackrepo.Update(tracking);

            await trackrepo.Save();

            return mapper.Map<ShipmentTrackingDTO>(tracking);
        }


    }
}
 
