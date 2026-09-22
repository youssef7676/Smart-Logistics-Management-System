using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Warehouses.Commands.CreateWarehouseCommand
{
    public class CreateDataWarehouseCommand :IRequest<int>
    {
        public string Name { get; set; }


        public string Location { get; set; }


        public int Capacity { get; set; }
    }
}
