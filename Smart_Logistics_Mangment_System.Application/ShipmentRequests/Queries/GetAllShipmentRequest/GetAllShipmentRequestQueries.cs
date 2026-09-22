using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentRequests.Queries.GetAllShipmentRequest
{
    public class GetAllShipmentRequestQueries :IRequest<List<ShipmentRequestDTO>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
