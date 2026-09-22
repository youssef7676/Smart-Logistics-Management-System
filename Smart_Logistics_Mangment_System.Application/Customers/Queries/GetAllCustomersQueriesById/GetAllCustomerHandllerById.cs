using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.Customers.DTOs;
using Smart_Logistics_Mangment_System.Application.Vehicles.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Customers.Queries.GetAllCustomerQueriesById
{
    public class GetAllCustomerHandllerById(
     ICustomerReposatory custrepo,
     ICurrentUserService currentUserService,
     IMapper mapper)
     : IRequestHandler<GetAllCustomerQueriesById, CustomersDTO>
    {
        public async Task<CustomersDTO> Handle(
            GetAllCustomerQueriesById request,
            CancellationToken cancellationToken)
        {
            var role = currentUserService.Role;
            var userId = currentUserService.UserId;

            Customer customer =
                await custrepo.GetAllCustomersById(
                    request.Id,
                    "User");

            if (customer == null)
                return null;

            // Admin + Employee
            if (role == "Admin" || role == "Employee")
            {
                return mapper.Map<CustomersDTO>(customer);
            }

            // Customer
            if (role == "Customer")
            {
                if (customer.UserId != userId)
                {
                    throw new UnauthorizedAccessException(
                        "You are not allowed to view this customer.");
                }

                return mapper.Map<CustomersDTO>(customer);
            }

            throw new UnauthorizedAccessException(
                "You are not allowed to view customers.");
        }
    }
}

        
