using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Domain.Models
{
    public class ShipmentItem : BaseEntity
    {
        public string ItemName { get; set; }


        public int Quantity { get; set; }


        public double Weight { get; set; }


        public string? Description { get; set; }



        // Relation with ShipmentRequest

        public int ShipmentRequestId { get; set; }

        public ShipmentRequest ShipmentRequest { get; set; }



        // Relation with Shipment (after approval)

        public int? ShipmentId { get; set; }

        public Shipment Shipment { get; set; }
    }
}
