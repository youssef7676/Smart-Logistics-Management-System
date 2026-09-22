using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Domain.Models
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; }


        public string Location { get; set; }


        public int Capacity { get; set; }


        public ICollection<Shipment> Shipments { get; set; }
    }
}
