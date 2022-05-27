using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Entities {
    
    
    public class EmployeePluralConfiguration : IEntityTypeConfiguration<EmployeePlural> {
        
        private string _schema = "dbo";
        
        public virtual void Configure(EntityTypeBuilder<EmployeePlural> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<EmployeePlural> builder, string schema) {
            builder.ToTable("Employees", schema);
            builder.HasKey(x => x.EmployeeID);

            builder.Property(x => x.EmployeeID).HasColumnName(@"EmployeeID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.LastName).HasColumnName(@"LastName").HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
            builder.Property(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType("nvarchar").IsRequired().HasMaxLength(10);
            builder.Property(x => x.Title).HasColumnName(@"Title").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.TitleOfCourtesy).HasColumnName(@"TitleOfCourtesy").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(25);
            builder.Property(x => x.BirthDate).HasColumnName(@"BirthDate").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.HireDate).HasColumnName(@"HireDate").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.Address).HasColumnName(@"Address").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(60);
            builder.Property(x => x.City).HasColumnName(@"City").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(15);
            builder.Property(x => x.Region).HasColumnName(@"Region").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(15);
            builder.Property(x => x.PostalCode).HasColumnName(@"PostalCode").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.Country).HasColumnName(@"Country").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(15);
            builder.Property(x => x.HomePhone).HasColumnName(@"HomePhone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(24);
            builder.Property(x => x.Extension).HasColumnName(@"Extension").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(4);
            builder.Property(x => x.Photo).HasColumnName(@"Photo").HasColumnType("image").IsRequired(false);
            builder.Property(x => x.Notes).HasColumnName(@"Notes").HasColumnType("ntext").IsRequired(false);
            builder.Property(x => x.ReportsTo).HasColumnName(@"ReportsTo").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.PhotoPath).HasColumnName(@"PhotoPath").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(255);

            //Foreign keys
            builder.HasOne(a => a.Employee).WithMany(b => b.EmployeePlurals).HasForeignKey(c => c.ReportsTo).OnDelete(DeleteBehavior.Restrict); // FK_Employees_Employees
        }
    }
}

