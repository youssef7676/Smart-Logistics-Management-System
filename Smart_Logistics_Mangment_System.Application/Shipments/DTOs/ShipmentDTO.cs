using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Shipments.DTOs
{
    public class ShipmentDTO
    {
        public int Id { get; set; }

        public int ShipmentRequestId { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int PickupWarehouseId { get; set; }
        public string PickupWarehouseName { get; set; }

        public string DestinationAddress { get; set; }

        public double TotalWeight { get; set; }

        public ShipmentStatus Status { get; set; }

        public ShipmentPriority Priority { get; set; }

        public int? DriverId { get; set; }
        public string DriverName { get; set; }

        public int? VehicleId { get; set; }
        public string VehiclePlateNumber { get; set; }

        public DateTime? PickedUpAt { get; set; }

        public DateTime? DeliveredAt { get; set; }

        public List<ShipmentItemDTO> Items { get; set; } = new();
    }
}

