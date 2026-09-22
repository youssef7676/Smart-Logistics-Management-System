using AutoMapper;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using Smart_Logistics_Mangment_System.Application.Shipments.Commands.CreateShipment;
using Smart_Logistics_Mangment_System.Application.Shipments.Commands.DeleteShipment;
using Smart_Logistics_Mangment_System.Application.Shipments.Commands.UpdateShipment;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.Shipments.DTOs
{
    public class ShipmentProfile : Profile
    {
        public ShipmentProfile()
        {
            CreateMap<ShipmentItem, ShipmentItemDTO>();

            CreateMap<Shipment, ShipmentDTO>()
                .ForMember(
                    dest => dest.CustomerName,
                    opt => opt.MapFrom(
                        src => src.Customer.User.FullName))

                .ForMember(
                    dest => dest.PickupWarehouseName,
                    opt => opt.MapFrom(
                        src => src.PickupWarehouse.Name))

                .ForMember(
                    dest => dest.DriverName,
                    opt => opt.MapFrom(
                        src => src.Driver != null
                            ? src.Driver.User.FullName
                            : null))

                .ForMember(
                    dest => dest.VehiclePlateNumber,
                    opt => opt.MapFrom(
                        src => src.Vehicle != null
                            ? src.Vehicle.PlateNumber
                            : null));

            CreateMap<Shipment, CreateShipmentCommand>().ReverseMap();
            CreateMap<Shipment, UpdateShipmentCommand>().ReverseMap();
            CreateMap<Shipment, DeleteShipmentCommand>().ReverseMap();



        }
    }
}
