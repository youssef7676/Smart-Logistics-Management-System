using AutoMapper;
using Smart_Logistics_Mangment_System.Application.Customers.Commands.CreateCustomerCommand;
using Smart_Logistics_Mangment_System.Application.Customers.Commands.DeleteCustomerCommand;
using Smart_Logistics_Mangment_System.Application.Customers.Commands.UpdateCustomerCommand;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Customers.DTOs
{
    public class CustomersProfile :Profile
    {
        public CustomersProfile()
        {
            CreateMap<Customer, CustomersDTO>()
                .ForMember(dest => dest.FullName, opt => opt
                .MapFrom(src => src.User.FullName)).ReverseMap();
            CreateMap<Customer, CreateCustomersCommand>().ReverseMap();
            CreateMap<Customer, DeleteCustomersCommand>().ReverseMap();
            CreateMap<Customer, UpdateCustomersCommand>().ReverseMap();




        }
    }
}
