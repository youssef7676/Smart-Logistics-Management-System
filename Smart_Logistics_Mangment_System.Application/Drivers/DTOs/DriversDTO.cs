using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Drivers.DTOs
{
    public class DriversDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string LicenseNumber { get; set; }

        public int? VehicleId { get; set; }

        public string? VehiclePlateNumber { get; set; }
    }
}
