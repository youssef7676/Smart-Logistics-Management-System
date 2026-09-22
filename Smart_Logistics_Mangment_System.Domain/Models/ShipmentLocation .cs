using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Domain.Models
{
    public class ShipmentLocation : BaseEntity
    {
        public int ShipmentId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public DateTime RecordedAt { get; set; }

        public Shipment Shipment { get; set; }
    }
}
