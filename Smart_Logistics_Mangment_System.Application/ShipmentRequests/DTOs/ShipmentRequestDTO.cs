using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs
{
    public class ShipmentRequestDTO
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int PickupWarehouseId { get; set; }

        public string PickupWarehouseName { get; set; }

        public string DestinationAddress { get; set; }

        public ShipmentRequestStatus Status { get; set; }

        public List<ShipmentItemDTO> Items { get; set; }

        public int? ShipmentId { get; set; }
    }
}
