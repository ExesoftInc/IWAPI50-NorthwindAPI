using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindAPI.Services {
    
    
    public interface ITerritoryPluralBuilder {
        
        Task<IQueryable<TerritoryPluralModel>> GetTerritoryPlurals();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetTerritoryPlural_ByTerritoryID(string territoryID);
        
        Task<IQueryable<TerritoryPluralModel>> GetTerritoryPlural_ByRegionID(int regionID);
        
        Task<BuilderResponse> AddTerritoryPlural(TerritoryPluralModel model);
        
        Task<BuilderResponse> UpdateTerritoryPlural(TerritoryPluralModel model);
        
        Task<BuilderResponse> DeleteTerritoryPlural(string territoryID);
    }
}

