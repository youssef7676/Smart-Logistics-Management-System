using MediatR;
using Smart_Logistics_Mangment_System.Application.Employees.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Employees.Queries.GetAllEmployee
{
    public class GetAllEmployeesQueries :IRequest<List<EmployeeDTO>>
    {
    }
}
