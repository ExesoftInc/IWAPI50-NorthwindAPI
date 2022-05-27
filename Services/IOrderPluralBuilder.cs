using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindAPI.Services {
    
    
    public interface IOrderPluralBuilder {
        
        Task<IQueryable<OrderPluralModel>> GetOrderPlurals();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetOrderPlural_ByOrderID(int orderID);
        
        Task<IQueryable<OrderPluralModel>> GetOrderPlural_ByCustomerID(string customerID);
        
        Task<IQueryable<OrderPluralModel>> GetOrderPlural_ByEmployeeID(int employeeID);
        
        Task<IQueryable<OrderPluralModel>> GetOrderPlural_ByShipVia(int shipVia);
        
        Task<BuilderResponse> AddOrderPlural(OrderPluralModel model);
        
        Task<BuilderResponse> UpdateOrderPlural(OrderPluralModel model);
        
        Task<BuilderResponse> DeleteOrderPlural(int orderID);
    }
}

