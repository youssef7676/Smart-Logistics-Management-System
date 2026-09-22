using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Smart_Logistics_Mangment_System.Application.Notifications;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Infrastruction.DB_Context;
using Smart_Logistics_Mangment_System.Infrastruction.Reposatories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Infrastruction.Extentions
{
    public static class ExtentionsMethod
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfigurationManager cfgmanager)
        {
            services.AddDbContext<Application_Context>(option =>
            {
                option.UseSqlServer(cfgmanager.GetConnectionString("CS"));
            });

            services.AddScoped<IAppUserReposatory, AppUserRepo>();
            services.AddScoped<ICustomerReposatory, CustomerRepo>();
            services.AddScoped<IDriverReposatory, DriverRepo>();
            services.AddScoped<IEmployeeReposatory, EmployeeRepo>();
            services.AddScoped<IShipmentItemReposatory, ShipmentItemRepo>();
            services.AddScoped<IShipmentReposatory, ShipmentRepo>();
            services.AddScoped<IShipmentRequestItemReposatory, ShipmentRequestRepo>();
            services.AddScoped<IShipmentTrackingReposatory, ShipmentTrackingRepo>();
            services.AddScoped<IVehicleReposatory, VehicleRepo>();
            services.AddScoped<IWarehouseReposatory, WarehouseRepo>();
            services.AddScoped<IRefreshTokenReposatory, RefreshTokenRepo>();
            services.AddScoped<IJwtReposatory, JWTRepo>();
            services.AddScoped<IETAService, ETARepo>();









        }
    }
}
