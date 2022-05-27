using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindAPI.Services {
    
    
    public interface IProductPluralBuilder {
        
        Task<IQueryable<ProductPluralModel>> GetProductPlurals();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetProductPlural_ByProductID(int productID);
        
        Task<IQueryable<ProductPluralModel>> GetProductPlural_BySupplierID(int supplierID);
        
        Task<IQueryable<ProductPluralModel>> GetProductPlural_ByCategoryID(int categoryID);
        
        Task<BuilderResponse> AddProductPlural(ProductPluralModel model);
        
        Task<BuilderResponse> UpdateProductPlural(ProductPluralModel model);
        
        Task<BuilderResponse> DeleteProductPlural(int productID);
    }
}

