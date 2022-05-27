using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindAPI.Services {
    
    
    public interface IEmployeeTerritoryPluralBuilder {
        
        Task<IQueryable<EmployeeTerritoryPluralModel>> GetEmployeeTerritoryPlurals();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetEmployeeTerritoryPlural_ByEmployeeIDTerritoryID(int employeeID, string territoryID);
        
        Task<IQueryable<EmployeeTerritoryPluralModel>> GetEmployeeTerritoryPlural_ByEmployeeID(int employeeID);
        
        Task<IQueryable<EmployeeTerritoryPluralModel>> GetEmployeeTerritoryPlural_ByTerritoryID(string territoryID);
        
        Task<BuilderResponse> AddEmployeeTerritoryPlural(EmployeeTerritoryPluralModel model);
        
        Task<BuilderResponse> UpdateEmployeeTerritoryPlural(EmployeeTerritoryPluralModel model);
        
        Task<BuilderResponse> DeleteEmployeeTerritoryPlural(int employeeID, string territoryID);
    }
}

