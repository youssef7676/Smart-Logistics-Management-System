using Smart_Logistics_Mangment_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Domain.Models
{
    public class Vehicle : BaseEntity
    {
        public string PlateNumber { get; set; }

        public string Model { get; set; }


        public double Capacity { get; set; }


        public VehicleStatus Status { get; set; }


        public Driver Driver { get; set; }


        public ICollection<Shipment> Shipments { get; set; }
    }
}
