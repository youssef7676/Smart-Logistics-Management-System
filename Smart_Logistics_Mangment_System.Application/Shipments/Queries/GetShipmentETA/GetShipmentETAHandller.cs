using MediatR;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Queries.GetShipmentETA
{
    public class GetShipmentETAQuery : IRequest<ShipmentETADTO>
    {
        public int ShipmentId { get; set; }
    }
}
