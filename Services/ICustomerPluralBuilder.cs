using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindAPI.Services {
    
    
    public interface ICustomerPluralBuilder {
        
        Task<IQueryable<CustomerPluralModel>> GetCustomerPlurals();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetCustomerPlural_ByCustomerID(string customerID);
        
        Task<BuilderResponse> AddCustomerPlural(CustomerPluralModel model);
        
        Task<BuilderResponse> UpdateCustomerPlural(CustomerPluralModel model);
        
        Task<BuilderResponse> DeleteCustomerPlural(string customerID);
    }
}

