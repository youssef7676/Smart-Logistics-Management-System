using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Domain.Enums;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Employees.Commands.CreateEmployeeCommand
{
    public class CreateEmployeesHandller(
    IEmployeeReposatory emprepo,
    IAppUserReposatory userrepo)
    : IRequestHandler<CreateEmployeesCommand, int>
    {
        public async Task<int> Handle(
            CreateEmployeesCommand request,
            CancellationToken cancellationToken)
        {
            // Check Email
            var users = await userrepo.GetAll();

            var emailExists = users.Any(x =>
                x.Email != null &&
                x.Email.ToLower() == request.Email.ToLower());

            if (emailExists)
            {
                throw new Exception("Email already exists.");
            }

            // Create AppUser
            var user = new AppUser
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = UserRole.Employee,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await userrepo.Add(user);
            await userrepo.Save();

            // Create Employee
            var employee = new Employee
            {
                UserId = user.Id,
                Department = request.Department,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await emprepo.Add(employee);
            await emprepo.Save();

            return employee.Id;
        }
    }
}