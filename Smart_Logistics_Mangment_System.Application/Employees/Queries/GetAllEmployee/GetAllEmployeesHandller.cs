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

namespace Smart_Logistics_Mangment_System.Application.Employees.Queries.GetAllEmployee
{
    public class GetAllEmployeesHandller(IEmployeeReposatory emprepo, IMapper mapper)
        : IRequestHandler<GetAllEmployeesQueries, List<EmployeeDTO>>
    {
        public async Task<List<EmployeeDTO>> Handle(GetAllEmployeesQueries request, CancellationToken cancellationToken)
        {
            List<Employee> employees = await emprepo.GetAll("User");
            List<EmployeeDTO> empdto = mapper.Map<List<EmployeeDTO>>(employees);
            return empdto;
        }
    }
}
