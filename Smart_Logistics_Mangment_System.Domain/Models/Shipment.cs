using Smart_Logistics_Mangment_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Domain.Models
{
    public class Shipment : BaseEntity
    {

        // Original Request

        public int ShipmentRequestId { get; set; }

        public ShipmentRequest ShipmentRequest { get; set; }



        // Customer

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }



        // Warehouse pickup

        public int PickupWarehouseId { get; set; }

        public Warehouse PickupWarehouse { get; set; }



        // Delivery information

        public string DestinationAddress { get; set; }



        // Shipment details

        public double TotalWeight { get; set; }


        public ShipmentStatus Status { get; set; }


        public ShipmentPriority Priority { get; set; }



        // Driver

        public int? DriverId { get; set; }

        public Driver Driver { get; set; }



        // Vehicle

        public int? VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }



        // Dates

        public DateTime? PickedUpAt { get; set; }

        public DateTime? DeliveredAt { get; set; }



        public decimal? DestinationLatitude { get; set; }

        public decimal? DestinationLongitude { get; set; }

        public ICollection<ShipmentLocation> LocationHistory { get; set; } = new List<ShipmentLocation>();


        // Items

        public ICollection<ShipmentItem> Items { get; set; } = new List<ShipmentItem>();


        // Tracking

        public ICollection<ShipmentTracking> TrackingHistory { get; set; } = new List<ShipmentTracking>();
    }
}
