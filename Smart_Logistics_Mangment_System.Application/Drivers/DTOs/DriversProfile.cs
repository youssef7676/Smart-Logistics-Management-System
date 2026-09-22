using AutoMapper;
using Smart_Logistics_Mangment_System.Application.Drivers.Commands.CreateDriversCommand;
using Smart_Logistics_Mangment_System.Application.Drivers.Commands.DeleteDriversCommand;
using Smart_Logistics_Mangment_System.Application.Drivers.Commands.UpdateDriversCommand;
using Smart_Logistics_Mangment_System.Application.Warehouses.Commands.CreateWarehouseCommand;
using Smart_Logistics_Mangment_System.Application.Warehouses.DTOs;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Drivers.DTOs
{
    public class DriversProfile : Profile
    {
        public DriversProfile()
        {
            CreateMap<Driver, DriversDTO>()
                        .ForMember(dest => dest.FullName,
                            opt => opt.MapFrom(src => src.User.FullName))
                        .ForMember(dest => dest.VehiclePlateNumber,
                            opt => opt.MapFrom(src => src.Vehicle != null ? src.Vehicle.PlateNumber : null));

            CreateMap<Driver, CreateDriverCommand>().ReverseMap();
            CreateMap<Driver, UpdateDriverCommand>().ReverseMap();
            CreateMap<Driver, DeleteDriverCommand>().ReverseMap();



        }
    }
    
}
