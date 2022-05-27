using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Entities {
    
    
    public class CategoryPluralConfiguration : IEntityTypeConfiguration<CategoryPlural> {
        
        private string _schema = "dbo";
        
        public virtual void Configure(EntityTypeBuilder<CategoryPlural> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<CategoryPlural> builder, string schema) {
            builder.ToTable("Categories", schema);
            builder.HasKey(x => x.CategoryID);

            builder.Property(x => x.CategoryID).HasColumnName(@"CategoryID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.CategoryName).HasColumnName(@"CategoryName").HasColumnType("nvarchar").IsRequired().HasMaxLength(15);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType("ntext").IsRequired(false);
            builder.Property(x => x.Picture).HasColumnName(@"Picture").HasColumnType("image").IsRequired(false);

            //Foreign keys
        }
    }
}

