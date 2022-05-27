using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Entities {
    
    
    public class OrderPluralConfiguration : IEntityTypeConfiguration<OrderPlural> {
        
        private string _schema = "dbo";
        
        public virtual void Configure(EntityTypeBuilder<OrderPlural> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<OrderPlural> builder, string schema) {
            builder.ToTable("Orders", schema);
            builder.HasKey(x => x.OrderID);

            builder.Property(x => x.OrderID).HasColumnName(@"OrderID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.CustomerID).HasColumnName(@"CustomerID").HasColumnType("nchar").IsRequired(false).IsFixedLength().HasMaxLength(5);
            builder.Property(x => x.EmployeeID).HasColumnName(@"EmployeeID").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OrderDate).HasColumnName(@"OrderDate").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.RequiredDate).HasColumnName(@"RequiredDate").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.ShippedDate).HasColumnName(@"ShippedDate").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.ShipVia).HasColumnName(@"ShipVia").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Freight).HasColumnName(@"Freight").HasColumnType("money").IsRequired(false).HasColumnType("decimal19,4)");
            builder.Property(x => x.ShipName).HasColumnName(@"ShipName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(40);
            builder.Property(x => x.ShipAddress).HasColumnName(@"ShipAddress").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(60);
            builder.Property(x => x.ShipCity).HasColumnName(@"ShipCity").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(15);
            builder.Property(x => x.ShipRegion).HasColumnName(@"ShipRegion").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(15);
            builder.Property(x => x.ShipPostalCode).HasColumnName(@"ShipPostalCode").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.ShipCountry).HasColumnName(@"ShipCountry").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(15);

            //Foreign keys
            builder.HasOne(a => a.Customer).WithMany(b => b.OrderPlurals).HasForeignKey(c => c.CustomerID).OnDelete(DeleteBehavior.Restrict); // FK_Orders_Customers
            builder.HasOne(a => a.Employee).WithMany(b => b.OrderPlurals).HasForeignKey(c => c.EmployeeID).OnDelete(DeleteBehavior.Restrict); // FK_Orders_Employees
            builder.HasOne(a => a.Shipper).WithMany(b => b.OrderPlurals).HasForeignKey(c => c.ShipVia).OnDelete(DeleteBehavior.Restrict); // FK_Orders_Shippers
        }
    }
}

