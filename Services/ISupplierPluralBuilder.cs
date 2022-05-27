using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindAPI.Services {
    
    
    public interface ISupplierPluralBuilder {
        
        Task<IQueryable<SupplierPluralModel>> GetSupplierPlurals();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetSupplierPlural_BySupplierID(int supplierID);
        
        Task<BuilderResponse> AddSupplierPlural(SupplierPluralModel model);
        
        Task<BuilderResponse> UpdateSupplierPlural(SupplierPluralModel model);
        
        Task<BuilderResponse> DeleteSupplierPlural(int supplierID);
    }
}

