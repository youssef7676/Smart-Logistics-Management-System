using MediatR;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Commands.AssignShipment
{
    public class AssignShipmentCommand : IRequest<ShipmentDTO>
    {
        public int ShipmentId { get; set; }

        public int DriverId { get; set; }
    }
}