using NorthwindAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindAPI.Services {
    
    
    public interface IEmployeePluralBuilder {
        
        Task<IQueryable<EmployeePluralModel>> GetEmployeePlurals();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetEmployeePlural_ByEmployeeID(int employeeID);
        
        Task<IQueryable<EmployeePluralModel>> GetEmployeePlural_ByReportsTo(int reportsTo);
        
        Task<BuilderResponse> AddEmployeePlural(EmployeePluralModel model);
        
        Task<BuilderResponse> UpdateEmployeePlural(EmployeePluralModel model);
        
        Task<BuilderResponse> DeleteEmployeePlural(int employeeID);
    }
}

