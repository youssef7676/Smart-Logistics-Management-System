using MediatR;
using Smart_Logistics_Mangment_System.Application.Warehouses.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Warehouses.Commands.DeleteWarehouseCommand
{
    public class DeleteWarehouseCommands :IRequest<DriversDTO>
    {
        public int Id { get; set; }
    }
}
