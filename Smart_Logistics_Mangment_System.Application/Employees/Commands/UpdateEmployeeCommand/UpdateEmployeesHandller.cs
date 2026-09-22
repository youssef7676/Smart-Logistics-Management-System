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

namespace Smart_Logistics_Mangment_System.Application.Employees.Commands.UpdateEmployeeCommand
{
    public class UpdateEmployeesHandller(
    IEmployeeReposatory emprepo,
    IMapper mapper)
    : IRequestHandler<UpdateEmployeesCommand, EmployeeDTO>
    {
        public async Task<EmployeeDTO> Handle(
            UpdateEmployeesCommand request,
            CancellationToken cancellationToken)
        {
            Employee employee =
                await emprepo.GetAllEmployeesById(request.Id,"User");

            if (employee == null)
                return null;

            employee.Department = request.Department;

            if (employee.User != null)
            {
                employee.User.FullName = request.FullName;
            }

            emprepo.Update(employee);
            await emprepo.Save();

            return mapper.Map<EmployeeDTO>(employee);
        }
    }
}
