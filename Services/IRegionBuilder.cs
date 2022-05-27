using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindAPI.Services {
    
    
    public interface IRegionBuilder {
        
        Task<IQueryable<RegionModel>> GetRegions();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetRegion_ByRegionID(int regionID);
        
        Task<BuilderResponse> AddRegion(RegionModel model);
        
        Task<BuilderResponse> UpdateRegion(RegionModel model);
        
        Task<BuilderResponse> DeleteRegion(int regionID);
    }
}

