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
    public class ShipmentRequestConfiguration
    : IEntityTypeConfiguration<ShipmentRequest>
    {
        public void Configure(EntityTypeBuilder<ShipmentRequest> builder)
        {
            builder.Property(x => x.DestinationAddress)
                .HasMaxLength(500);


            builder.HasOne(x => x.Customer)
                .WithMany(x => x.ShipmentRequests)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(x => x.Items)
                .WithOne(x => x.ShipmentRequest)
                .HasForeignKey(x => x.ShipmentRequestId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(x => x.Shipment)
                .WithOne(x => x.ShipmentRequest)
                .HasForeignKey<Shipment>(x => x.ShipmentRequestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
