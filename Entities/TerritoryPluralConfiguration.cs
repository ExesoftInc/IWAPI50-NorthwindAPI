using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Entities {
    
    
    public class TerritoryPluralConfiguration : IEntityTypeConfiguration<TerritoryPlural> {
        
        private string _schema = "dbo";
        
        public virtual void Configure(EntityTypeBuilder<TerritoryPlural> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<TerritoryPlural> builder, string schema) {
            builder.ToTable("Territories", schema);
            builder.HasKey(x => x.TerritoryID);

            builder.Property(x => x.TerritoryID).HasColumnName(@"TerritoryID").HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
            builder.Property(x => x.TerritoryDescription).HasColumnName(@"TerritoryDescription").HasColumnType("nchar").IsRequired().IsFixedLength().HasMaxLength(50);
            builder.Property(x => x.RegionID).HasColumnName(@"RegionID").HasColumnType("int").IsRequired();

            //Foreign keys
            builder.HasOne(a => a.Region).WithMany(b => b.TerritoryPlurals).HasForeignKey(c => c.RegionID).OnDelete(DeleteBehavior.Restrict); // FK_Territories_Region
        }
    }
}

