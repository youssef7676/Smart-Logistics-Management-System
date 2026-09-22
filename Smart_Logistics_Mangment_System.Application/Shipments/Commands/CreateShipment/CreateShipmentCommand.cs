using MediatR;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Commands.CreateShipment
{
    public class CreateShipmentCommand :IRequest<ShipmentDTO>
    {
        public int ShipmentRequestId { get; set; }

        public decimal DestinationLatitude { get; set; }

        public decimal DestinationLongitude { get; set; }
        public ShipmentPriority Priority { get; set; }
    }
}
