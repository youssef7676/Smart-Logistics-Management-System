using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Customers.Commands.CreateCustomerCommand
{
    public class CreateCustomersCommand : IRequest<int>
    {
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
    }
}
