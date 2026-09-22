using MediatR;
using Smart_Logistics_Mangment_System.Application.Customers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Customers.Queries.GetAllCustomerQueries
{
    public class GetAllCustomerQueries :IRequest<List<CustomersDTO>>
    {
    }
}
