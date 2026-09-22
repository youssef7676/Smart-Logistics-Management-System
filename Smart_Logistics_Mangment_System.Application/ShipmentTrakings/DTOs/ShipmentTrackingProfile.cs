using AutoMapper;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.Commands.CreateShipmentTracking;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.Commands.DeleteShipmentTracking;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.Commands.UpdateShipmentTracking;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.ShipmentTrakings.DTOs
{
    public class ShipmentTrackingProfile :Profile
    {
        public ShipmentTrackingProfile()
        {
            CreateMap<ShipmentTracking, ShipmentTrackingDTO>();
            CreateMap<CreateShipmentTrackingCommand, ShipmentTracking>();
            CreateMap<UpdateShipmentTrackingCommand, ShipmentTracking>();
            CreateMap<DeleteShipmentTrackingCommand, ShipmentTracking>();




        }
    }
}
