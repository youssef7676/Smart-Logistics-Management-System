using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs
{
    public class ShipmentItemDTO
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public double Weight { get; set; }

        public string? Description { get; set; }
        public int ShipmentRequestId { get; set; }
        public int? ShipmentId { get; set; }
    }
}
