using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Entities {
    
    
    public class SupplierPluralConfiguration : IEntityTypeConfiguration<SupplierPlural> {
        
        private string _schema = "dbo";
        
        public virtual void Configure(EntityTypeBuilder<SupplierPlural> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<SupplierPlural> builder, string schema) {
            builder.ToTable("Suppliers", schema);
            builder.HasKey(x => x.SupplierID);

            builder.Property(x => x.SupplierID).HasColumnName(@"SupplierID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.CompanyName).HasColumnName(@"CompanyName").HasColumnType("nvarchar").IsRequired().HasMaxLength(40);
            builder.Property(x => x.ContactName).HasColumnName(@"ContactName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.ContactTitle).HasColumnName(@"ContactTitle").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.Address).HasColumnName(@"Address").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(60);
            builder.Property(x => x.City).HasColumnName(@"City").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(15);
            builder.Property(x => x.Region).HasColumnName(@"Region").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(15);
            builder.Property(x => x.PostalCode).HasColumnName(@"PostalCode").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.Country).HasColumnName(@"Country").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(15);
            builder.Property(x => x.Phone).HasColumnName(@"Phone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(24);
            builder.Property(x => x.Fax).HasColumnName(@"Fax").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(24);
            builder.Property(x => x.HomePage).HasColumnName(@"HomePage").HasColumnType("ntext").IsRequired(false);

            //Foreign keys
        }
    }
}

