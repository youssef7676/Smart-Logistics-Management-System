using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands.DeleteShipmentRequest
{
    public class DeleteShipmentRequestCommand :IRequest<ShipmentRequestDTO>
    {
        public int Id { get; set; }
    }
}
