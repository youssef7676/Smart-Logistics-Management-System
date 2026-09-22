using MediatR;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Commands.UpdateShipment
{
    public class UpdateShipmentCommand : IRequest<ShipmentDTO>
    {
        public int Id { get; set; }

        public string DestinationAddress { get; set; }

        public ShipmentPriority Priority { get; set; }
    }
}
