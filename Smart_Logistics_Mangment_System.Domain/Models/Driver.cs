using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Domain.Models
{
    public class Driver : BaseEntity
    {
        public int UserId { get; set; }

        public AppUser User { get; set; }


        public string LicenseNumber { get; set; }


        public int? VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }


        public ICollection<Shipment> Shipments { get; set; }
    }
}
