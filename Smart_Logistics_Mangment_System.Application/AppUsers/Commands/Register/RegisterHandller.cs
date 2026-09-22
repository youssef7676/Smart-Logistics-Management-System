using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.AppUsers.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.AppUsers.Commands.Register
{
    public class RegisterHandller(IAppUserReposatory userrepo, ICustomerReposatory custrepo, IMapper mapper)
        : IRequestHandler<RegisterCommand, int>
    {
        public async Task<int> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Check Email
            var users = await userrepo.GetAll();

            var emailExists = users.Any(x =>
                x.Email.ToLower() == request.Email.ToLower());

            if (emailExists)
                throw new Exception("Email already exists.");

            // Create AppUser
            var user = new AppUser
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = UserRole.Customer
            };

            await userrepo.Add(user);

            await userrepo.Save();

            Console.WriteLine($"USER ID AFTER SAVE = {user.Id}");

            var customer = new Customer
            {
                UserId = user.Id,
                CompanyName = request.CompanyName,
                Address = request.Address
            };

            // Add Customer
            await custrepo.Add(customer);

            // Save Customer
            await custrepo.Save();

            return user.Id;
        }
    }
}