using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Shipments.Queries.GetAllShipments
{
    public class GetAllShipmentQueries: IRequest<List<ShipmentDTO>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
