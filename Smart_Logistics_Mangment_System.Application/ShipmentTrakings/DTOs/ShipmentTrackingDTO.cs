using Smart_Logistics_Mangment_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentTrakings.DTOs
{
    public class ShipmentTrackingDTO
    {
        public int Id { get; set; }

        public int ShipmentId { get; set; }

        public ShipmentStatus Status { get; set; }

        public string Location { get; set; }

        public string? Note { get; set; }
    }
}
