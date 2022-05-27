using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindAPI.Services {
    
    
    public interface ICustomerDemographicPluralBuilder {
        
        Task<IQueryable<CustomerDemographicPluralModel>> GetCustomerDemographicPlurals();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetCustomerDemographicPlural_ByCustomerTypeID(string customerTypeID);
        
        Task<BuilderResponse> AddCustomerDemographicPlural(CustomerDemographicPluralModel model);
        
        Task<BuilderResponse> UpdateCustomerDemographicPlural(CustomerDemographicPluralModel model);
        
        Task<BuilderResponse> DeleteCustomerDemographicPlural(string customerTypeID);
    }
}

