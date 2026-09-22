using MediatR;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Queries.GetAllShipmentsById
{
    public class GetAllShipmentByIdQueries :IRequest<ShipmentDTO>
    {
        public int Id { get; set; }
        public GetAllShipmentByIdQueries(int id)
        {
            Id = id;
        }
    }
}
