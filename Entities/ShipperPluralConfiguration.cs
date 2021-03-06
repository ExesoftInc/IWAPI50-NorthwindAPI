using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Entities {
    
    
    public class ShipperPluralConfiguration : IEntityTypeConfiguration<ShipperPlural> {
        
        private string _schema = "dbo";
        
        public virtual void Configure(EntityTypeBuilder<ShipperPlural> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<ShipperPlural> builder, string schema) {
            builder.ToTable("Shippers", schema);
            builder.HasKey(x => x.ShipperID);

            builder.Property(x => x.ShipperID).HasColumnName(@"ShipperID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.CompanyName).HasColumnName(@"CompanyName").HasColumnType("nvarchar").IsRequired().HasMaxLength(40);
            builder.Property(x => x.Phone).HasColumnName(@"Phone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(24);

            //Foreign keys
        }
    }
}

