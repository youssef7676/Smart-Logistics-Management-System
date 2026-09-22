using MediatR;
using Smart_Logistics_Mangment_System.Application.Employees.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Employees.Queries.GetAllEmployeeById
{
    public class GetAllEmployeeQueriesById :IRequest<EmployeeDTO>
    {
        public int Id { get; set; }
        public GetAllEmployeeQueriesById(int id)
        {
            Id = id;
        }
    }
}
