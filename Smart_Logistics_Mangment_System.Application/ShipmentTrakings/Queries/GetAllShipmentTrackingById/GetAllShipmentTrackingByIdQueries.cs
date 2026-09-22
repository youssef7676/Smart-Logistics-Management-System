using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentTrakings.Queries.GetAllShipmentTrackingById
{
    public class GetAllShipmentTrackingByIdQueries :IRequest<ShipmentTrackingDTO>
    {
        public int Id { get; set; }
        public GetAllShipmentTrackingByIdQueries(int id)
        {
            Id = id;
        }
    }
}
