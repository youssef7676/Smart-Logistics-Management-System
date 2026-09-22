using MediatR;
using Smart_Logistics_Mangment_System.Domain.Enums;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;

namespace Smart_Logistics_Mangment_System.Application.Customers.Commands.CreateCustomerCommand
{
    public class CreateCustomersHandller(
        ICustomerReposatory custrepo,
        IAppUserReposatory userrepo,
        ICurrentUserService currentUserService)
        : IRequestHandler<CreateCustomersCommand, int>
    {
        public async Task<int> Handle(
            CreateCustomersCommand request,
            CancellationToken cancellationToken)
        {
            var role = currentUserService.Role;

            if (role != "Admin" && role != "Employee")
            {
                throw new UnauthorizedAccessException(
                    "You are not allowed to create customers.");
            }

            // Create User
            var user = new AppUser
            {
                FullName = request.FullName,
                Role = UserRole.Customer,
                CreatedAt = DateTime.UtcNow
            };

            await userrepo.Add(user);
            await userrepo.Save();

            // Create Customer
            var customer = new Customer
            {
                UserId = user.Id,
                CompanyName = request.CompanyName,
                Address = request.Address
            };

            await custrepo.Add(customer);
            await custrepo.Save();

            return customer.Id;
        }
    }
}