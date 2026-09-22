using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smart_Logistics_Mangment_System.Domain.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Infrastruction.Configurations
{
    public class DriverConfiguration
     : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.Property(x => x.LicenseNumber)
                .HasMaxLength(100);


            builder.HasOne(x => x.Vehicle)
                .WithOne(x => x.Driver)
                .HasForeignKey<Driver>(x => x.VehicleId)
                .OnDelete(DeleteBehavior.SetNull);


            builder.HasMany(x => x.Shipments)
                .WithOne(x => x.Driver)
                .HasForeignKey(x => x.DriverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
