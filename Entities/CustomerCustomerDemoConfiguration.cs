using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Entities {
    
    
    public class CustomerCustomerDemoConfiguration : IEntityTypeConfiguration<CustomerCustomerDemo> {
        
        private string _schema = "dbo";
        
        public virtual void Configure(EntityTypeBuilder<CustomerCustomerDemo> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<CustomerCustomerDemo> builder, string schema) {
            builder.ToTable("CustomerCustomerDemo", schema);
            builder.HasKey(x => new { x.CustomerID, x.CustomerTypeID });

            builder.Property(x => x.CustomerID).HasColumnName(@"CustomerID").HasColumnType("nchar").IsRequired().IsFixedLength().HasMaxLength(5);
            builder.Property(x => x.CustomerTypeID).HasColumnName(@"CustomerTypeID").HasColumnType("nchar").IsRequired().IsFixedLength().HasMaxLength(10);

            //Foreign keys
            builder.HasOne(a => a.Customer).WithMany(b => b.CustomerCustomerDemoes).HasForeignKey(c => c.CustomerID).OnDelete(DeleteBehavior.Restrict); // FK_CustomerCustomerDemo_Customers
            builder.HasOne(a => a.CustomerDemographic).WithMany(b => b.CustomerCustomerDemoes).HasForeignKey(c => c.CustomerTypeID).OnDelete(DeleteBehavior.Restrict); // FK_CustomerCustomerDemo
        }
    }
}

