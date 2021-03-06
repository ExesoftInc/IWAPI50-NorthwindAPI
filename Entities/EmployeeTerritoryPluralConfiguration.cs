using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Entities {
    
    
    public class EmployeeTerritoryPluralConfiguration : IEntityTypeConfiguration<EmployeeTerritoryPlural> {
        
        private string _schema = "dbo";
        
        public virtual void Configure(EntityTypeBuilder<EmployeeTerritoryPlural> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<EmployeeTerritoryPlural> builder, string schema) {
            builder.ToTable("EmployeeTerritories", schema);
            builder.HasKey(x => new { x.EmployeeID, x.TerritoryID });

            builder.Property(x => x.EmployeeID).HasColumnName(@"EmployeeID").HasColumnType("int").IsRequired();
            builder.Property(x => x.TerritoryID).HasColumnName(@"TerritoryID").HasColumnType("nvarchar").IsRequired().HasMaxLength(20);

            //Foreign keys
            builder.HasOne(a => a.Employee).WithMany(b => b.EmployeeTerritoryPlurals).HasForeignKey(c => c.EmployeeID).OnDelete(DeleteBehavior.Restrict); // FK_EmployeeTerritories_Employees
            builder.HasOne(a => a.Territory).WithMany(b => b.EmployeeTerritoryPlurals).HasForeignKey(c => c.TerritoryID).OnDelete(DeleteBehavior.Restrict); // FK_EmployeeTerritories_Territories
        }
    }
}

