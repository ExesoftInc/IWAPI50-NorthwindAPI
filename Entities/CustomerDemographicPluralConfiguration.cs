using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Entities {
    
    
    public class CustomerDemographicPluralConfiguration : IEntityTypeConfiguration<CustomerDemographicPlural> {
        
        private string _schema = "dbo";
        
        public virtual void Configure(EntityTypeBuilder<CustomerDemographicPlural> builder) {
            Configure(builder, _schema);
        }
        
        private void Configure(EntityTypeBuilder<CustomerDemographicPlural> builder, string schema) {
            builder.ToTable("CustomerDemographics", schema);
            builder.HasKey(x => x.CustomerTypeID);

            builder.Property(x => x.CustomerTypeID).HasColumnName(@"CustomerTypeID").HasColumnType("nchar").IsRequired().IsFixedLength().HasMaxLength(10);
            builder.Property(x => x.CustomerDesc).HasColumnName(@"CustomerDesc").HasColumnType("ntext").IsRequired(false);

            //Foreign keys
        }
    }
}

