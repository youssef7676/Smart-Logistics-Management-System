using MediatR;
using Smart_Logistics_Mangment_System.Application.Drivers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Drivers.Queries.GetAllDriversById
{
    public class GetAllDriversQueriesById :IRequest<DriversDTO>
    {
        public int Id { get; set; }

        public GetAllDriversQueriesById(int id)
        {
            Id = id;
        }
    }
}
