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
    
    
    public class CustomerCustomerDemoBuilder : ICustomerCustomerDemoBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public CustomerCustomerDemoBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<CustomerCustomerDemo, CustomerCustomerDemoModel>>  ProjectToModel {
            get {
                return entity => new CustomerCustomerDemoModel(entity);
            }
        }
        
        public async Task<IQueryable<CustomerCustomerDemoModel>> GetCustomerCustomerDemoes() {
            return await Task.FromResult(_entities.CustomerCustomerDemoes.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.CustomerCustomerDemoes.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetCustomerCustomerDemo_ByCustomerIDCustomerTypeID(string customerID, string customerTypeID) {
            var query = await Search(_entities.CustomerCustomerDemoes, x => x.CustomerID == customerID&& x.CustomerTypeID == customerTypeID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; CustomerCustomerDemo with customerID, customerTypeID = '{customerID}', '{customerTypeID}' doesn't exist." }; 
            }
        }
        
        public async Task<IQueryable<CustomerCustomerDemoModel>> GetCustomerCustomerDemo_ByCustomerID(string customerID) {

            var query = await Task.FromResult(Search(_entities.CustomerCustomerDemoes, x => x.CustomerID == customerID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<IQueryable<CustomerCustomerDemoModel>> GetCustomerCustomerDemo_ByCustomerTypeID(string customerTypeID) {

            var query = await Task.FromResult(Search(_entities.CustomerCustomerDemoes, x => x.CustomerTypeID == customerTypeID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddCustomerCustomerDemo(CustomerCustomerDemoModel model) {

            var matchCustomerID = _entities.CustomerPlurals.Where(x => x.CustomerID.Equals(model.CustomerID));
            if (!matchCustomerID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.CustomerID) + " '{model.CustomerID}' doesn't exist in the system."}; 
            }

            var matchCustomerTypeID = _entities.CustomerDemographicPlurals.Where(x => x.CustomerTypeID.Equals(model.CustomerTypeID));
            if (!matchCustomerTypeID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.CustomerTypeID) + " '{model.CustomerTypeID}' doesn't exist in the system."}; 
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.CustomerCustomerDemoes.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CustomerCustomerDemo added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new CustomerCustomerDemoModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateCustomerCustomerDemo(CustomerCustomerDemoModel model) {

            var query = Search(_entities.CustomerCustomerDemoes, x =>  x.CustomerID == model.CustomerID && x.CustomerTypeID == model.CustomerTypeID);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("CustomerCustomerDemo with _customerID, _customerTypeID = '{0}', '{1}' doesn't exist.",model.CustomerID, model.CustomerTypeID)}; 
            }

            var matchCustomerID = _entities.CustomerPlurals.Where(x => x.CustomerID.Equals(model.CustomerID));
            if (!matchCustomerID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.CustomerID) + string.Format("CustomerID = '{0}' doesn't exist in the system.", model.CustomerID)}; 
            }

            var matchCustomerTypeID = _entities.CustomerDemographicPlurals.Where(x => x.CustomerTypeID.Equals(model.CustomerTypeID));
            if (!matchCustomerTypeID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.CustomerTypeID) + string.Format("CustomerTypeID = '{0}' doesn't exist in the system.", model.CustomerTypeID)}; 
            }

            CustomerCustomerDemo entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CustomerCustomerDemo update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteCustomerCustomerDemo(string customerID, string customerTypeID) {

            var query = Search(_entities.CustomerCustomerDemoes, x => x.CustomerID == customerID&& x.CustomerTypeID == customerTypeID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("CustomerCustomerDemo with _customerID, _customerTypeID = '{0}', '{1}' doesn't exist.",customerID, customerTypeID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.CustomerCustomerDemoes.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CustomerCustomerDemo deleted with values: '{0}'", JsonConvert.SerializeObject(new CustomerCustomerDemoModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<CustomerCustomerDemo> Search(IQueryable<CustomerCustomerDemo> query, Expression<Func<CustomerCustomerDemo, bool>> filter) {
            return query.Where(filter);
        }
    }
}

