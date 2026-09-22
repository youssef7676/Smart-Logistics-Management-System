using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Infrastruction.DB_Context
{
    public class Application_Context : DbContext
    {
        public Application_Context(DbContextOptions<Application_Context> options)
        : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<ShipmentRequest> ShipmentRequests { get; set; }

        public DbSet<ShipmentItem> ShipmentItems { get; set; }

        public DbSet<Shipment> Shipments { get; set; }

        public DbSet<ShipmentTracking> ShipmentTrackings { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<ShipmentLocation> ShipmentLocations { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.ApplyConfigurationsFromAssembly(
                typeof(Application_Context).Assembly);
        }
    }
}
