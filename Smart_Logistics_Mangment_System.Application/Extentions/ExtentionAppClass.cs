using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Smart_Logistics_Mangment_System.Application.RefreshToken;
using Smart_Logistics_Mangment_System.Application.Warehouses.DTOs;
using Smart_Logistics_Mangment_System.Application.Vehicles.DTOs;
using Smart_Logistics_Mangment_System.Application.Drivers.DTOs;
using Smart_Logistics_Mangment_System.Application.Customers.DTOs;
using Smart_Logistics_Mangment_System.Application.Employees.DTOs;
using Smart_Logistics_Mangment_System.Application.AppUsers.DTOs;
using Smart_Logistics_Mangment_System.Application.AppUsers.Commands;
using Smart_Logistics_Mangment_System.Application.ShipmentRequests.DTOs;
using Smart_Logistics_Mangment_System.Application.ShipmentItems.DTOs;
using Smart_Logistics_Mangment_System.Application.Shipments.DTOs;
using Smart_Logistics_Mangment_System.Application.ShipmentTrakings.DTOs;

namespace Smart_Logistics_Mangment_System.Application.Extentions
{
    public static class ExtentionAppClass
    {

        public static void ApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<WarehouseProfile>();
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<VehicleProfile>();
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DriversProfile>();
            });


            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CustomersProfile>();
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<EmployeeProfile>();
            });


            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AppUserProfile>();
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ShipmentRequestProfile>();
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ShipmentItemProfile>();
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ShipmentProfile>();
            });
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ShipmentTrackingProfile>();
            });


        }
    }
}
