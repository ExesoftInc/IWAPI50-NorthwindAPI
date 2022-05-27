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
    
    
    public class CustomerDemographicPluralBuilder : ICustomerDemographicPluralBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public CustomerDemographicPluralBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<CustomerDemographicPlural, CustomerDemographicPluralModel>>  ProjectToModel {
            get {
                return entity => new CustomerDemographicPluralModel(entity);
            }
        }
        
        public async Task<IQueryable<CustomerDemographicPluralModel>> GetCustomerDemographicPlurals() {
            return await Task.FromResult(_entities.CustomerDemographicPlurals.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.CustomerDemographicPlurals.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetCustomerDemographicPlural_ByCustomerTypeID(string customerTypeID) {
            var query = await Search(_entities.CustomerDemographicPlurals, x => x.CustomerTypeID == customerTypeID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; CustomerDemographicPlural with customerTypeID = '{customerTypeID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddCustomerDemographicPlural(CustomerDemographicPluralModel model) {


            var entity = ModelExtender.ToEntity(model);
            _entities.CustomerDemographicPlurals.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CustomerDemographicPlural added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new CustomerDemographicPluralModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateCustomerDemographicPlural(CustomerDemographicPluralModel model) {

            var query = Search(_entities.CustomerDemographicPlurals, x =>  x.CustomerTypeID == model.CustomerTypeID);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("CustomerDemographicPlural with _customerTypeID = '{0}' doesn't exist.",model.CustomerTypeID)}; 
            }

            CustomerDemographicPlural entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CustomerDemographicPlural update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteCustomerDemographicPlural(string customerTypeID) {

            var query = Search(_entities.CustomerDemographicPlurals, x => x.CustomerTypeID == customerTypeID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("CustomerDemographicPlural with _customerTypeID = '{0}' doesn't exist.",customerTypeID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.CustomerDemographicPlurals.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CustomerDemographicPlural deleted with values: '{0}'", JsonConvert.SerializeObject(new CustomerDemographicPluralModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<CustomerDemographicPlural> Search(IQueryable<CustomerDemographicPlural> query, Expression<Func<CustomerDemographicPlural, bool>> filter) {
            return query.Where(filter);
        }
    }
}

