using AutoMapper;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.Commands.CreateShipmentItem;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.Commands.DeleteShipmentItem;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.Commands.UpdateShipmentItem;
using Smart_Logistics_Mangment_System.Application.Vehicles.DTOs;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs
{
    public class ShipmentItemProfile :Profile
    {
        public ShipmentItemProfile()
        {
            CreateMap<ShipmentItem, ShipmentItemDTO>().ReverseMap();
            CreateMap<CreateShipmentItemCommand, ShipmentItem>();
            CreateMap<UpdateShipmentItemCommand, ShipmentItem>();
            CreateMap<DeleteShipmentItemCommand, ShipmentItem>();

        }
    }
}
