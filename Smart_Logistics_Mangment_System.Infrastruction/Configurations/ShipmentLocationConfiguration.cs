using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smart_Logistics_Mangment_System.Domain.Models;

namespace Smart_Logistics_Mangment_System.Infrastruction.Configurations
{
    public class ShipmentLocationConfiguration
        : IEntityTypeConfiguration<ShipmentLocation>
    {
        public void Configure(
            EntityTypeBuilder<ShipmentLocation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Latitude)
                .HasPrecision(9, 6)
                .IsRequired();

            builder.Property(x => x.Longitude)
                .HasPrecision(9, 6)
                .IsRequired();

            builder.Property(x => x.RecordedAt)
                .IsRequired();

            builder.HasOne(x => x.Shipment)
                .WithMany(x => x.LocationHistory)
                .HasForeignKey(x => x.ShipmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
