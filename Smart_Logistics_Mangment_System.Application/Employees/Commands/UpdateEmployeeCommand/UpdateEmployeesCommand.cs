using MediatR;
using Smart_Logistics_Mangment_System.Application.Employees.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Employees.Commands.UpdateEmployeeCommand
{
    public class UpdateEmployeesCommand :IRequest<EmployeeDTO>
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Department { get; set; }
    }
}
