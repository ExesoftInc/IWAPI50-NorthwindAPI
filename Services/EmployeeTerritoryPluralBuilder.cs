using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NorthwindAPI.Entities;
using NorthwindAPI.Models;
using NorthwindAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace NorthwindAPI.Services {
    
    
    public class EmployeeTerritoryPluralBuilder : IEmployeeTerritoryPluralBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public EmployeeTerritoryPluralBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<EmployeeTerritoryPlural, EmployeeTerritoryPluralModel>>  ProjectToModel {
            get {
                return entity => new EmployeeTerritoryPluralModel(entity);
            }
        }
        
        public async Task<IQueryable<EmployeeTerritoryPluralModel>> GetEmployeeTerritoryPlurals() {
            return await Task.FromResult(_entities.EmployeeTerritoryPlurals.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.EmployeeTerritoryPlurals.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetEmployeeTerritoryPlural_ByEmployeeIDTerritoryID(int employeeID, string territoryID) {
            var query = await Search(_entities.EmployeeTerritoryPlurals, x => x.EmployeeID == employeeID&& x.TerritoryID == territoryID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; EmployeeTerritoryPlural with employeeID, territoryID = '{employeeID}', '{territoryID}' doesn't exist." }; 
            }
        }
        
        public async Task<IQueryable<EmployeeTerritoryPluralModel>> GetEmployeeTerritoryPlural_ByEmployeeID(int employeeID) {

            var query = await Task.FromResult(Search(_entities.EmployeeTerritoryPlurals, x => x.EmployeeID == employeeID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<IQueryable<EmployeeTerritoryPluralModel>> GetEmployeeTerritoryPlural_ByTerritoryID(string territoryID) {

            var query = await Task.FromResult(Search(_entities.EmployeeTerritoryPlurals, x => x.TerritoryID == territoryID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddEmployeeTerritoryPlural(EmployeeTerritoryPluralModel model) {

            var matchEmployeeID = _entities.EmployeePlurals.Where(x => x.EmployeeID.Equals(model.EmployeeID));
            if (!matchEmployeeID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.EmployeeID) + " '{model.EmployeeID}' doesn't exist in the system."}; 
            }

            var matchTerritoryID = _entities.TerritoryPlurals.Where(x => x.TerritoryID.Equals(model.TerritoryID));
            if (!matchTerritoryID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.TerritoryID) + " '{model.TerritoryID}' doesn't exist in the system."}; 
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.EmployeeTerritoryPlurals.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("EmployeeTerritoryPlural added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new EmployeeTerritoryPluralModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateEmployeeTerritoryPlural(EmployeeTerritoryPluralModel model) {

            var query = Search(_entities.EmployeeTerritoryPlurals, x =>  x.EmployeeID == model.EmployeeID && x.TerritoryID == model.TerritoryID);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("EmployeeTerritoryPlural with _employeeID, _territoryID = '{0}', '{1}' doesn't exist.",model.EmployeeID, model.TerritoryID)}; 
            }

            var matchEmployeeID = _entities.EmployeePlurals.Where(x => x.EmployeeID.Equals(model.EmployeeID));
            if (!matchEmployeeID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.EmployeeID) + string.Format("EmployeeID = '{0}' doesn't exist in the system.", model.EmployeeID)}; 
            }

            var matchTerritoryID = _entities.TerritoryPlurals.Where(x => x.TerritoryID.Equals(model.TerritoryID));
            if (!matchTerritoryID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.TerritoryID) + string.Format("TerritoryID = '{0}' doesn't exist in the system.", model.TerritoryID)}; 
            }

            EmployeeTerritoryPlural entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("EmployeeTerritoryPlural update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteEmployeeTerritoryPlural(int employeeID, string territoryID) {

            var query = Search(_entities.EmployeeTerritoryPlurals, x => x.EmployeeID == employeeID&& x.TerritoryID == territoryID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("EmployeeTerritoryPlural with _employeeID, _territoryID = '{0}', '{1}' doesn't exist.",employeeID, territoryID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.EmployeeTerritoryPlurals.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("EmployeeTerritoryPlural deleted with values: '{0}'", JsonConvert.SerializeObject(new EmployeeTerritoryPluralModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<EmployeeTerritoryPlural> Search(IQueryable<EmployeeTerritoryPlural> query, Expression<Func<EmployeeTerritoryPlural, bool>> filter) {
            return query.Where(filter);
        }
    }
}

