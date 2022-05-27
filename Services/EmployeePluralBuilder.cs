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
    
    
    public class EmployeePluralBuilder : IEmployeePluralBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public EmployeePluralBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<EmployeePlural, EmployeePluralModel>>  ProjectToModel {
            get {
                return entity => new EmployeePluralModel(entity);
            }
        }
        
        public async Task<IQueryable<EmployeePluralModel>> GetEmployeePlurals() {
            return await Task.FromResult(_entities.EmployeePlurals.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.EmployeePlurals.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetEmployeePlural_ByEmployeeID(int employeeID) {
            var query = await Search(_entities.EmployeePlurals, x => x.EmployeeID == employeeID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; EmployeePlural with employeeID = '{employeeID}' doesn't exist." }; 
            }
        }
        
        public async Task<IQueryable<EmployeePluralModel>> GetEmployeePlural_ByReportsTo(int reportsTo) {

            var query = await Task.FromResult(Search(_entities.EmployeePlurals, x => x.ReportsTo == reportsTo).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddEmployeePlural(EmployeePluralModel model) {

            if (model.ReportsTo != null) {
                var matchReportsTo = _entities.EmployeePlurals.Where(x => x.EmployeeID.Equals(model.ReportsTo));
                if (!matchReportsTo.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.ReportsTo) + " '{model.ReportsTo}' doesn't exist in the system."}; 
                }
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.EmployeePlurals.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("EmployeePlural added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new EmployeePluralModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateEmployeePlural(EmployeePluralModel model) {

            var query = Search(_entities.EmployeePlurals, x =>  x.EmployeeID == model.EmployeeID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("EmployeePlural with _employeeID = '{0}' doesn't exist.",model.EmployeeID)}; 
            }

            if (model.ReportsTo != null) {
                var matchReportsTo = _entities.EmployeePlurals.Where(x => x.EmployeeID.Equals(model.ReportsTo));
                if (!matchReportsTo.Any()) {
                return new BuilderResponse{ ValidationMessage = "Foreign Key Violation; " + nameof(model.ReportsTo) + string.Format("EmployeePlural with ReportsTo = '{0}' doesn't exist.", model.ReportsTo)}; 
                }
            }

            EmployeePlural entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("EmployeePlural update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteEmployeePlural(int employeeID) {

            var query = Search(_entities.EmployeePlurals, x => x.EmployeeID == employeeID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("EmployeePlural with _employeeID = '{0}' doesn't exist.",employeeID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.EmployeePlurals.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("EmployeePlural deleted with values: '{0}'", JsonConvert.SerializeObject(new EmployeePluralModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<EmployeePlural> Search(IQueryable<EmployeePlural> query, Expression<Func<EmployeePlural, bool>> filter) {
            return query.Where(filter);
        }
    }
}

