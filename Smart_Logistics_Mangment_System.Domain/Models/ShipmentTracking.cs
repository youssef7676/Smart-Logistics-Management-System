using Smart_Logistics_Mangment_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Domain.Models
{
    public class ShipmentTracking : BaseEntity
    {
        // Shipment

        public int ShipmentId { get; set; }

        public Shipment Shipment { get; set; }



        // Current status

        public ShipmentStatus Status { get; set; }



        // Location information

        public string Location { get; set; }



        // Additional information

        public string? Note { get; set; }
    }
}
