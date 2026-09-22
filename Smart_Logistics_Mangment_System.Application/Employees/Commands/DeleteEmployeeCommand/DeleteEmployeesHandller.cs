using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.Customers.DTOs;
using Smart_Logistics_Mangment_System.Application.Employees.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Employees.Commands.DeleteEmployeeCommand
{
    public class DeleteEmployeesHandller(
     IEmployeeReposatory emprepo,
     IMapper mapper)
     : IRequestHandler<DeleteEmployeesCommand, EmployeeDTO>
    {
        public async Task<EmployeeDTO> Handle(
            DeleteEmployeesCommand request,
            CancellationToken cancellationToken)
        {
            Employee employee =
                await emprepo.GetById(request.Id);

            if (employee == null)
                return null;

            await emprepo.Delete(employee.Id);
            await emprepo.Save();

            return mapper.Map<EmployeeDTO>(employee);
        }
    }
}
