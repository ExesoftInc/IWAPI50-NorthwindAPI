using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Entities {
    
    
    public class ProductPluralConfiguration : IEntityTypeConfiguration<ProductPlural> {
        
        private string _schema = "dbo";
        
        public virtual void Configure(EntityTypeBuilder<ProductPlural> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<ProductPlural> builder, string schema) {
            builder.ToTable("Products", schema);
            builder.HasKey(x => x.ProductID);

            builder.Property(x => x.ProductID).HasColumnName(@"ProductID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.ProductName).HasColumnName(@"ProductName").HasColumnType("nvarchar").IsRequired().HasMaxLength(40);
            builder.Property(x => x.SupplierID).HasColumnName(@"SupplierID").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CategoryID).HasColumnName(@"CategoryID").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.QuantityPerUnit).HasColumnName(@"QuantityPerUnit").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(20);
            builder.Property(x => x.UnitPrice).HasColumnName(@"UnitPrice").HasColumnType("money").IsRequired(false).HasColumnType("decimal19,4)");
            builder.Property(x => x.UnitsInStock).HasColumnName(@"UnitsInStock").HasColumnType("smallint").IsRequired(false);
            builder.Property(x => x.UnitsOnOrder).HasColumnName(@"UnitsOnOrder").HasColumnType("smallint").IsRequired(false);
            builder.Property(x => x.ReorderLevel).HasColumnName(@"ReorderLevel").HasColumnType("smallint").IsRequired(false);
            builder.Property(x => x.Discontinued).HasColumnName(@"Discontinued").HasColumnType("bit").IsRequired();

            //Foreign keys
            builder.HasOne(a => a.Supplier).WithMany(b => b.ProductPlurals).HasForeignKey(c => c.SupplierID).OnDelete(DeleteBehavior.Restrict); // FK_Products_Suppliers
            builder.HasOne(a => a.Category).WithMany(b => b.ProductPlurals).HasForeignKey(c => c.CategoryID).OnDelete(DeleteBehavior.Restrict); // FK_Products_Categories
        }
    }
}

