using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindAPI.Services {
    
    
    public interface IOrderDetailPluralBuilder {
        
        Task<IQueryable<OrderDetailPluralModel>> GetOrderDetailPlurals();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetOrderDetailPlural_ByOrderIDProductID(int orderID, int productID);
        
        Task<IQueryable<OrderDetailPluralModel>> GetOrderDetailPlural_ByOrderID(int orderID);
        
        Task<IQueryable<OrderDetailPluralModel>> GetOrderDetailPlural_ByProductID(int productID);
        
        Task<BuilderResponse> AddOrderDetailPlural(OrderDetailPluralModel model);
        
        Task<BuilderResponse> UpdateOrderDetailPlural(OrderDetailPluralModel model);
        
        Task<BuilderResponse> DeleteOrderDetailPlural(int orderID, int productID);
    }
}

