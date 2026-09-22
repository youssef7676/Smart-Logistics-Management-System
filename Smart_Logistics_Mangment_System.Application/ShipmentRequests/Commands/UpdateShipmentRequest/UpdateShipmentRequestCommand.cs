using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands.UpdateShipmentRequest
{
    public class UpdateShipmentRequestCommand :IRequest<ShipmentRequestDTO>
    {
        public int Id { get; set; }

        public int PickupWarehouseId { get; set; }

        public string DestinationAddress { get; set; }

        public List<ShipmentItemDTO> Items { get; set; } = new();
    }
}
