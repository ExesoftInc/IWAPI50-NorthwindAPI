using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindAPI.Services {
    
    
    public interface ICategoryPluralBuilder {
        
        Task<IQueryable<CategoryPluralModel>> GetCategoryPlurals();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetCategoryPlural_ByCategoryID(int categoryID);
        
        Task<BuilderResponse> AddCategoryPlural(CategoryPluralModel model);
        
        Task<BuilderResponse> UpdateCategoryPlural(CategoryPluralModel model);
        
        Task<BuilderResponse> DeleteCategoryPlural(int categoryID);
    }
}

