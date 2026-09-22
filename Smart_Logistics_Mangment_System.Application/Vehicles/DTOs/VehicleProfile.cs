using AutoMapper;
using Smart_Logistics_Mangment_System.Application.Vehicles.Commands.CreateVehicleCommand;
using Smart_Logistics_Mangment_System.Application.Vehicles.Commands.DeleteVehicleCommand;
using Smart_Logistics_Mangment_System.Application.Vehicles.Commands.UpdateVehicleCommand;
using Smart_Logistics_Mangment_System.Application.Warehouses.DTOs;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Vehicles.DTOs
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<Vehicle, VehicleDTO>().ReverseMap();
            CreateMap<Vehicle, CreateVehiclesCommands>().ReverseMap();
            CreateMap<Vehicle, UpdateVehiclesCommand>().ReverseMap();
            CreateMap<Vehicle, DeleteVehicleCommand>().ReverseMap();




        }
    }
}
