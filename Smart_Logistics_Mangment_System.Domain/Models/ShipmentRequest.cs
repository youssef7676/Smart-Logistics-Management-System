using Smart_Logistics_Mangment_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Domain.Models
{
    public class ShipmentRequest : BaseEntity
    {
        // Customer who created the request
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }


        // Pickup location

        public int PickupWarehouseId { get; set; }

        public Warehouse PickupWarehouse { get; set; }


        // Delivery information

        public string DestinationAddress { get; set; }


        // Request status

        public ShipmentRequestStatus Status { get; set; }


        // Items inside the request

        public ICollection<ShipmentItem> Items { get; set; } = new List<ShipmentItem>();


        // Created shipment after approval

        public int? ShipmentId { get; set; }

        public Shipment Shipment { get; set; }
    }
}
