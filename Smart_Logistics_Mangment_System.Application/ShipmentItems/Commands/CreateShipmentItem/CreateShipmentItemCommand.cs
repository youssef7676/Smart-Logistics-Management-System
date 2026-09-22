using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentItems.Commands.CreateShipmentItem
{
    public class CreateShipmentItemCommand : IRequest<ShipmentItemDTO>
    {
        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public double Weight { get; set; }

        public string? Description { get; set; }

        public int ShipmentRequestId { get; set; }
    }
}
