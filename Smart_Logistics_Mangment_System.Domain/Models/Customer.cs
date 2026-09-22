using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Domain.Models
{
    public class Customer : BaseEntity
    {
        public int UserId { get; set; }

        public AppUser User { get; set; }


        public string CompanyName { get; set; }

        public string Address { get; set; }

        public ICollection<ShipmentRequest> ShipmentRequests { get; set; } = new List<ShipmentRequest>();


        public ICollection<Shipment> Shipments { get; set; }
    }
}
