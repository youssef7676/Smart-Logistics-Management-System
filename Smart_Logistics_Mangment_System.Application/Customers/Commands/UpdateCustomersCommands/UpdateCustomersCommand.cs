using MediatR;
using Smart_Logistics_Mangment_System.Application.Customers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Customers.Commands.UpdateCustomerCommand
{
    public class UpdateCustomersCommand :IRequest<CustomersDTO>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }

    }
}
