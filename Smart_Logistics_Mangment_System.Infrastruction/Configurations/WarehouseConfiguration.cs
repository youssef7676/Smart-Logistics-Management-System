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
    public class WarehouseConfiguration
    : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(150);


            builder.Property(x => x.Location)
                .HasMaxLength(300);


            builder.HasMany(x => x.Shipments)
                .WithOne(x => x.PickupWarehouse)
                .HasForeignKey(x => x.PickupWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
