using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.Customers.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Customers.Commands.DeleteCustomerCommand
{
    public class DeleteCustomersHandller(ICustomerReposatory custrepo , ICurrentUserService currentUserService, IMapper mapper)
        : IRequestHandler<DeleteCustomersCommand, CustomersDTO>
    {
        public async Task<CustomersDTO> Handle(
           DeleteCustomersCommand request,
           CancellationToken cancellationToken)
        {
            var role = currentUserService.Role;

            // =========================
            // ADMIN ONLY
            // =========================

            if (role != "Admin")
            {
                throw new UnauthorizedAccessException(
                    "You are not allowed to delete customers.");
            }


            // =========================
            // GET CUSTOMER
            // =========================

            Customer customer =
                await custrepo.GetById(request.Id);

            if (customer == null)
                return null;


            // =========================
            // DELETE
            // =========================

            await custrepo.Delete(customer.Id);

            await custrepo.Save();

            return mapper.Map<CustomersDTO>(customer);
        }
    }
}
