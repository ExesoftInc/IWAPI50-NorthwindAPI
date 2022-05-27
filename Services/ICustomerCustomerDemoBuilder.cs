using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindAPI.Services {
    
    
    public interface ICustomerCustomerDemoBuilder {
        
        Task<IQueryable<CustomerCustomerDemoModel>> GetCustomerCustomerDemoes();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetCustomerCustomerDemo_ByCustomerIDCustomerTypeID(string customerID, string customerTypeID);
        
        Task<IQueryable<CustomerCustomerDemoModel>> GetCustomerCustomerDemo_ByCustomerID(string customerID);
        
        Task<IQueryable<CustomerCustomerDemoModel>> GetCustomerCustomerDemo_ByCustomerTypeID(string customerTypeID);
        
        Task<BuilderResponse> AddCustomerCustomerDemo(CustomerCustomerDemoModel model);
        
        Task<BuilderResponse> UpdateCustomerCustomerDemo(CustomerCustomerDemoModel model);
        
        Task<BuilderResponse> DeleteCustomerCustomerDemo(string customerID, string customerTypeID);
    }
}

