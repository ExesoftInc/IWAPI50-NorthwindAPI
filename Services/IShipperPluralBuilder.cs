using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindAPI.Services {
    
    
    public interface IShipperPluralBuilder {
        
        Task<IQueryable<ShipperPluralModel>> GetShipperPlurals();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetShipperPlural_ByShipperID(int shipperID);
        
        Task<BuilderResponse> AddShipperPlural(ShipperPluralModel model);
        
        Task<BuilderResponse> UpdateShipperPlural(ShipperPluralModel model);
        
        Task<BuilderResponse> DeleteShipperPlural(int shipperID);
    }
}

