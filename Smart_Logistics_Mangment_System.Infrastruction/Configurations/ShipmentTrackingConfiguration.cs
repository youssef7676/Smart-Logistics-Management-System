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
    public class ShipmentTrackingConfiguration
     : IEntityTypeConfiguration<ShipmentTracking>
    {
        public void Configure(EntityTypeBuilder<ShipmentTracking> builder)
        {
            builder.Property(x => x.Location)
                .HasMaxLength(200);


            builder.Property(x => x.Note)
                .HasMaxLength(500);
        }
    }
}
