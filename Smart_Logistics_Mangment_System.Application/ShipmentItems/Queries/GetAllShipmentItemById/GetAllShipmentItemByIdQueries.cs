using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentItems.Queries.GetAllShipmentItemById
{
    public class GetAllShipmentItemByIdQueries :IRequest<ShipmentItemDTO>
    {
        public int Id { get; set; }
        public GetAllShipmentItemByIdQueries(int id)
        {
            Id = id;
        }
    }
}
