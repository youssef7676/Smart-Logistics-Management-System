using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.Customers.DTOs;
using Smart_Logistics_Mangment_System.Application.Customers.Queries.GetAllCustomerQueries;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;

public class GetAllCustomerHandller(
    ICustomerReposatory custrepo,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<GetAllCustomerQueries, List<CustomersDTO>>
{
    public async Task<List<CustomersDTO>> Handle(
        GetAllCustomerQueries request,
        CancellationToken cancellationToken)
    {
        var role = currentUserService.Role;
        var userId = currentUserService.UserId;

        // Admin + Employee
        // يشوفوا كل العملاء
        if (role == "Admin" || role == "Employee")
        {
            List<Customer> customers =
                await custrepo.GetAll("User");

            return mapper.Map<List<CustomersDTO>>(customers);
        }

        // Customer
        // يشوف نفسه فقط
        if (role == "Customer")
        {
            List<Customer> customers =
                await custrepo.GetAll("User");

            var myCustomer = customers
                .Where(x => x.UserId == userId)
                .ToList();

            return mapper.Map<List<CustomersDTO>>(myCustomer);
        }

        throw new UnauthorizedAccessException(
            "You are not allowed to view customers.");
    }
}
