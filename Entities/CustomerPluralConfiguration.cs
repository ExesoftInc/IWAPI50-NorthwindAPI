using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Entities {
    
    
    public class CustomerPluralConfiguration : IEntityTypeConfiguration<CustomerPlural> {
        
        private string _schema = "dbo";
        
        public virtual void Configure(EntityTypeBuilder<CustomerPlural> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<CustomerPlural> builder, string schema) {
            builder.ToTable("Customers", schema);
            builder.HasKey(x => x.CustomerID);

            builder.Property(x => x.CustomerID).HasColumnName(@"CustomerID").HasColumnType("nchar").IsRequired().IsFixedLength().HasMaxLength(5);
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

            //Foreign keys
        }
    }
}

