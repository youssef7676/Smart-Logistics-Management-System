using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.Customers.Commands.UpdateCustomerCommand;
using Smart_Logistics_Mangment_System.Application.Customers.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;

public class UpdateCustomersHandller(
    ICustomerReposatory custrepo,
    IAppUserReposatory userrepo,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<UpdateCustomersCommand, CustomersDTO>
{
    public async Task<CustomersDTO> Handle(
        UpdateCustomersCommand request,
        CancellationToken cancellationToken)
    {
        var role = currentUserService.Role;
        var userId = currentUserService.UserId;

        if (role != "Admin" &&
            role != "Employee" &&
            role != "Customer")
        {
            throw new UnauthorizedAccessException(
                "You are not allowed to update customers.");
        }

        Customer customer =
            await custrepo.GetAllCustomersById(
                request.Id,
                "User");

        if (customer == null)
            return null;

        // Customer can update himself only
        if (role == "Customer")
        {
            if (customer.UserId != userId)
            {
                throw new UnauthorizedAccessException(
                    "You are not allowed to update this customer.");
            }
        }

        customer.Address = request.Address;
        customer.CompanyName = request.CompanyName;

        if (customer.User != null)
        {
            customer.User.FullName = request.FullName;
        }

        custrepo.Update(customer);

        await custrepo.Save();

        return mapper.Map<CustomersDTO>(customer);
    }
}