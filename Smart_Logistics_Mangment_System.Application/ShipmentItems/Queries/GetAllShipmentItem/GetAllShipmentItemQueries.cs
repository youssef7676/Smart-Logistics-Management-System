using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentItems.Queries.GetAllShipmentItem
{
    public class GetAllShipmentItemQueries :IRequest<List<ShipmentItemDTO>>
    {
    }
}
