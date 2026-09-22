using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Employees.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Department { get; set; }
    }
}
