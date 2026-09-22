using MediatR;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Commands.UpdateShipmentLocation
{
    public class UpdateShipmentLocationCommand : IRequest<ShipmentETADTO>
    {
        public int ShipmentId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}