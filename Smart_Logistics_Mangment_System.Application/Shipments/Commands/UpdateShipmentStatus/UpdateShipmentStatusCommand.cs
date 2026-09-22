using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Commands.UpdateShipmentStatus
{
    public class UpdateShipmentStatusCommand : IRequest<ShipmentTrackingDTO>
    {
        public int ShipmentId { get; set; }

        public ShipmentStatus Status { get; set; }

        public string Location { get; set; }

        public string? Note { get; set; }
    }
}
