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
    
    
    public class CustomerPluralBuilder : ICustomerPluralBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public CustomerPluralBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<CustomerPlural, CustomerPluralModel>>  ProjectToModel {
            get {
                return entity => new CustomerPluralModel(entity);
            }
        }
        
        public async Task<IQueryable<CustomerPluralModel>> GetCustomerPlurals() {
            return await Task.FromResult(_entities.CustomerPlurals.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.CustomerPlurals.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetCustomerPlural_ByCustomerID(string customerID) {
            var query = await Search(_entities.CustomerPlurals, x => x.CustomerID == customerID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; CustomerPlural with customerID = '{customerID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddCustomerPlural(CustomerPluralModel model) {


            var entity = ModelExtender.ToEntity(model);
            _entities.CustomerPlurals.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CustomerPlural added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new CustomerPluralModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateCustomerPlural(CustomerPluralModel model) {

            var query = Search(_entities.CustomerPlurals, x =>  x.CustomerID == model.CustomerID);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("CustomerPlural with _customerID = '{0}' doesn't exist.",model.CustomerID)}; 
            }

            CustomerPlural entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CustomerPlural update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteCustomerPlural(string customerID) {

            var query = Search(_entities.CustomerPlurals, x => x.CustomerID == customerID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("CustomerPlural with _customerID = '{0}' doesn't exist.",customerID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.CustomerPlurals.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CustomerPlural deleted with values: '{0}'", JsonConvert.SerializeObject(new CustomerPluralModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<CustomerPlural> Search(IQueryable<CustomerPlural> query, Expression<Func<CustomerPlural, bool>> filter) {
            return query.Where(filter);
        }
    }
}

