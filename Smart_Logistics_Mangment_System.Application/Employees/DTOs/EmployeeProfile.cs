using AutoMapper;
using Smart_Logistics_Mangment_System.Application.Customers.Commands.CreateCustomerCommand;
using Smart_Logistics_Mangment_System.Application.Employees.Commands.DeleteEmployeeCommand;
using Smart_Logistics_Mangment_System.Application.Employees.Commands.UpdateEmployeeCommand;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Employees.DTOs
{
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>().ForMember
                (dest => dest.FullName, opt => opt
                .MapFrom(src => src.User.FullName));

            CreateMap<Employee, CreateCustomersCommand>().ReverseMap();
            CreateMap<Employee, DeleteEmployeesCommand>().ReverseMap();
            CreateMap<Employee, UpdateEmployeesCommand>().ReverseMap();



        }
    }
}
