using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Employees.Commands.CreateEmployeeCommand
{
    public class CreateEmployeesCommand : IRequest<int>
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Department { get; set; }
    }
}
