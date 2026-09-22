using MediatR;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentItems.Commands.DeleteShipmentItem
{
    public class DeleteShipmentItemCommand :IRequest<ShipmentItemDTO>
    {
        public int Id { get; set; }
    }
}
