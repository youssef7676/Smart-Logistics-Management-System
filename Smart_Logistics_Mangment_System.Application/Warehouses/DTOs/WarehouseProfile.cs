using AutoMapper;
using Smart_Logistics_Mangment_System.Application.Warehouses.Commands.CreateWarehouseCommand;
using Smart_Logistics_Mangment_System.Application.Warehouses.Commands.DeleteWarehouseCommand;
using Smart_Logistics_Mangment_System.Application.Warehouses.Commands.UpdateWarehouseCommand;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Warehouses.DTOs
{
    public class WarehouseProfile : Profile
    {
        public WarehouseProfile()
        {
        
            CreateMap<Warehouse, DriversDTO>().ReverseMap();
            CreateMap<Warehouse, CreateDataWarehouseCommand>().ReverseMap();
            CreateMap<Warehouse, UpdateWarehouseCommands>().ReverseMap();
            CreateMap<Warehouse, DeleteWarehouseCommands>().ReverseMap();



        }
    }
}
