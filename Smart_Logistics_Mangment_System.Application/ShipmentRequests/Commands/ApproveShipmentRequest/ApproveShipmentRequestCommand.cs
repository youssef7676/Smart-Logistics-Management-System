using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands.ApproveShipmentRequest
{
    public class ApproveShipmentRequestCommand : IRequest<ShipmentRequestDTO>
    {
        public int Id { get; set; }

        public ApproveShipmentRequestCommand(int id)
        {
            Id = id;
        }
    }
}
