using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentTrakings.Commands.UpdateShipmentTracking
{
    public class UpdateShipmentTrackingCommand : IRequest<ShipmentTrackingDTO>
    {
        public int Id { get; set; }

        public ShipmentStatus Status { get; set; }

        public string Location { get; set; }

        public string? Note { get; set; }
    }
}
