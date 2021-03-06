using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Services;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindAPI.Entities {
    
    
    public class EntitiesContext : DbContext, IDbEntities {
        
        public EntitiesContext() {
            //empty constructor
        }
        
        public EntitiesContext(DbContextOptions<EntitiesContext> options) : 
                base(options) {
        }
        
        public virtual DbSet<CategoryPlural> CategoryPlurals { get; set; }
        
        public virtual DbSet<CustomerCustomerDemo> CustomerCustomerDemoes { get; set; }
        
        public virtual DbSet<CustomerDemographicPlural> CustomerDemographicPlurals { get; set; }
        
        public virtual DbSet<CustomerPlural> CustomerPlurals { get; set; }
        
        public virtual DbSet<EmployeePlural> EmployeePlurals { get; set; }
        
        public virtual DbSet<EmployeeTerritoryPlural> EmployeeTerritoryPlurals { get; set; }
        
        public virtual DbSet<OrderDetailPlural> OrderDetailPlurals { get; set; }
        
        public virtual DbSet<OrderPlural> OrderPlurals { get; set; }
        
        public virtual DbSet<ProductPlural> ProductPlurals { get; set; }
        
        public virtual DbSet<Region> Regions { get; set; }
        
        public virtual DbSet<ShipperPlural> ShipperPlurals { get; set; }
        
        public virtual DbSet<SupplierPlural> SupplierPlurals { get; set; }
        
        public virtual DbSet<TerritoryPlural> TerritoryPlurals { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.AddInterceptors(new DbInterceptor(new LoggerManager()));
        }
        
        public virtual async Task<int> SaveChangesAsync() {
           return await SaveChangesAsync(new CancellationToken());
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryPluralConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerCustomerDemoConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerDemographicPluralConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerPluralConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeePluralConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeTerritoryPluralConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailPluralConfiguration());
            modelBuilder.ApplyConfiguration(new OrderPluralConfiguration());
            modelBuilder.ApplyConfiguration(new ProductPluralConfiguration());
            modelBuilder.ApplyConfiguration(new RegionConfiguration());
            modelBuilder.ApplyConfiguration(new ShipperPluralConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierPluralConfiguration());
            modelBuilder.ApplyConfiguration(new TerritoryPluralConfiguration());
        }
    }
}

