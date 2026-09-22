using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.Employees.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Employees.Queries.GetAllEmployeeById
{
    public class GetAllEmployeeHandllerById(IEmployeeReposatory emprepo, IMapper mapper)
        : IRequestHandler<GetAllEmployeeQueriesById, EmployeeDTO>
    {
        public async Task<EmployeeDTO> Handle(GetAllEmployeeQueriesById request, CancellationToken cancellationToken)
        {
            Employee employee = await emprepo.GetAllEmployeesById(request.Id, "User");

            if (employee == null)
                return null;

            return mapper.Map<EmployeeDTO>(employee);
        }
    }
}
