using AutoMapper;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.Commands.UpdateShipmentRequest;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs
{
    public class ShipmentRequestProfile :Profile
    {
        public ShipmentRequestProfile()
        {
            CreateMap<ShipmentItem, ShipmentItemDTO>();

            CreateMap<ShipmentRequest, ShipmentRequestDTO>()
                .ForMember(
                    dest => dest.CustomerName,
                    opt => opt.MapFrom(
                        src => src.Customer.User.FullName))
                .ForMember(
                    dest => dest.PickupWarehouseName,
                    opt => opt.MapFrom(
                        src => src.PickupWarehouse.Name));

            CreateMap<ShipmentRequest, UpdateShipmentRequestCommand>();

        }
    }
}
