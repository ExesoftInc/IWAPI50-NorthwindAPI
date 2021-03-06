using Microsoft.EntityFrameworkCore;
using System;

namespace NorthwindAPI.Entities {
    
    
    public interface IDbEntities : IDbEntityBase {
        
        DbSet<CategoryPlural> CategoryPlurals {
            get;
            set;
        }
        
        DbSet<CustomerCustomerDemo> CustomerCustomerDemoes {
            get;
            set;
        }
        
        DbSet<CustomerDemographicPlural> CustomerDemographicPlurals {
            get;
            set;
        }
        
        DbSet<CustomerPlural> CustomerPlurals {
            get;
            set;
        }
        
        DbSet<EmployeePlural> EmployeePlurals {
            get;
            set;
        }
        
        DbSet<EmployeeTerritoryPlural> EmployeeTerritoryPlurals {
            get;
            set;
        }
        
        DbSet<OrderDetailPlural> OrderDetailPlurals {
            get;
            set;
        }
        
        DbSet<OrderPlural> OrderPlurals {
            get;
            set;
        }
        
        DbSet<ProductPlural> ProductPlurals {
            get;
            set;
        }
        
        DbSet<Region> Regions {
            get;
            set;
        }
        
        DbSet<ShipperPlural> ShipperPlurals {
            get;
            set;
        }
        
        DbSet<SupplierPlural> SupplierPlurals {
            get;
            set;
        }
        
        DbSet<TerritoryPlural> TerritoryPlurals {
            get;
            set;
        }
    }
}

