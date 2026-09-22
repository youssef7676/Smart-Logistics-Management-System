using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Shipments.DTOs
{
    public class ShipmentETADTO
    {
        public int ShipmentId { get; set; }

        public decimal CurrentLatitude { get; set; }

        public decimal CurrentLongitude { get; set; }

        public decimal DestinationLatitude { get; set; }

        public decimal DestinationLongitude { get; set; }

        public double DistanceKm { get; set; }

        public int EstimatedMinutes { get; set; }

        public string EstimatedTime { get; set; } = string.Empty;
    }
}
