using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Entities {
    
    
    public class RegionConfiguration : IEntityTypeConfiguration<Region> {
        
        private string _schema = "dbo";
        
        public virtual void Configure(EntityTypeBuilder<Region> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<Region> builder, string schema) {
            builder.ToTable("Region", schema);
            builder.HasKey(x => x.RegionID);

            builder.Property(x => x.RegionID).HasColumnName(@"RegionID").HasColumnType("int").IsRequired();
            builder.Property(x => x.RegionDescription).HasColumnName(@"RegionDescription").HasColumnType("nchar").IsRequired().IsFixedLength().HasMaxLength(50);

            //Foreign keys
        }
    }
}

