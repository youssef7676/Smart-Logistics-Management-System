using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Domain.Models
{
    public class Employee : BaseEntity
    {
        public int UserId { get; set; }

        public AppUser User { get; set; }


        public string Department { get; set; }
    }
}
