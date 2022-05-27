using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Entities {
    
    
    public class OrderDetailPluralConfiguration : IEntityTypeConfiguration<OrderDetailPlural> {
        
        private string _schema = "dbo";
        
        public virtual void Configure(EntityTypeBuilder<OrderDetailPlural> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<OrderDetailPlural> builder, string schema) {
            builder.ToTable("Order Details", schema);
            builder.HasKey(x => new { x.OrderID, x.ProductID });

            builder.Property(x => x.Quantity).HasColumnName(@"Quantity").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.Discount).HasColumnName(@"Discount").HasColumnType("real").IsRequired();
            builder.Property(x => x.OrderID).HasColumnName(@"OrderID").HasColumnType("int").IsRequired();
            builder.Property(x => x.ProductID).HasColumnName(@"ProductID").HasColumnType("int").IsRequired();
            builder.Property(x => x.UnitPrice).HasColumnName(@"UnitPrice").HasColumnType("money").IsRequired().HasColumnType("decimal19,4)");

            //Foreign keys
            builder.HasOne(a => a.Order).WithMany(b => b.OrderDetailPlurals).HasForeignKey(c => c.OrderID).OnDelete(DeleteBehavior.Restrict); // FK_Order_Details_Orders
            builder.HasOne(a => a.Product).WithMany(b => b.OrderDetailPlurals).HasForeignKey(c => c.ProductID).OnDelete(DeleteBehavior.Restrict); // FK_Order_Details_Products
        }
    }
}

