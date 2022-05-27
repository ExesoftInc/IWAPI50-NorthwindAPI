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
    
    
    public class OrderPluralBuilder : IOrderPluralBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public OrderPluralBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<OrderPlural, OrderPluralModel>>  ProjectToModel {
            get {
                return entity => new OrderPluralModel(entity);
            }
        }
        
        public async Task<IQueryable<OrderPluralModel>> GetOrderPlurals() {
            return await Task.FromResult(_entities.OrderPlurals.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.OrderPlurals.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetOrderPlural_ByOrderID(int orderID) {
            var query = await Search(_entities.OrderPlurals, x => x.OrderID == orderID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; OrderPlural with orderID = '{orderID}' doesn't exist." }; 
            }
        }
        
        public async Task<IQueryable<OrderPluralModel>> GetOrderPlural_ByCustomerID(string customerID) {

            var query = await Task.FromResult(Search(_entities.OrderPlurals, x => x.CustomerID == customerID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<IQueryable<OrderPluralModel>> GetOrderPlural_ByEmployeeID(int employeeID) {

            var query = await Task.FromResult(Search(_entities.OrderPlurals, x => x.EmployeeID == employeeID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<IQueryable<OrderPluralModel>> GetOrderPlural_ByShipVia(int shipVia) {

            var query = await Task.FromResult(Search(_entities.OrderPlurals, x => x.ShipVia == shipVia).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddOrderPlural(OrderPluralModel model) {

            if (model.CustomerID != null) {
                var matchCustomerID = _entities.CustomerPlurals.Where(x => x.CustomerID.Equals(model.CustomerID));
                if (!matchCustomerID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.CustomerID) + " '{model.CustomerID}' doesn't exist in the system."}; 
                }
            }

            if (model.EmployeeID != null) {
                var matchEmployeeID = _entities.EmployeePlurals.Where(x => x.EmployeeID.Equals(model.EmployeeID));
                if (!matchEmployeeID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.EmployeeID) + " '{model.EmployeeID}' doesn't exist in the system."}; 
                }
            }

            if (model.ShipVia != null) {
                var matchShipVia = _entities.ShipperPlurals.Where(x => x.ShipperID.Equals(model.ShipVia));
                if (!matchShipVia.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.ShipVia) + " '{model.ShipVia}' doesn't exist in the system."}; 
                }
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.OrderPlurals.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("OrderPlural added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new OrderPluralModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateOrderPlural(OrderPluralModel model) {

            var query = Search(_entities.OrderPlurals, x =>  x.OrderID == model.OrderID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("OrderPlural with _orderID = '{0}' doesn't exist.",model.OrderID)}; 
            }

            if (model.CustomerID != null) {
                var matchCustomerID = _entities.CustomerPlurals.Where(x => x.CustomerID.Equals(model.CustomerID));
                if (!matchCustomerID.Any()) {
                return new BuilderResponse{ ValidationMessage = "Foreign Key Violation; " + nameof(model.CustomerID) + string.Format("OrderPlural with CustomerID = '{0}' doesn't exist.", model.CustomerID)}; 
                }
            }

            if (model.EmployeeID != null) {
                var matchEmployeeID = _entities.EmployeePlurals.Where(x => x.EmployeeID.Equals(model.EmployeeID));
                if (!matchEmployeeID.Any()) {
                return new BuilderResponse{ ValidationMessage = "Foreign Key Violation; " + nameof(model.EmployeeID) + string.Format("OrderPlural with EmployeeID = '{0}' doesn't exist.", model.EmployeeID)}; 
                }
            }

            if (model.ShipVia != null) {
                var matchShipVia = _entities.ShipperPlurals.Where(x => x.ShipperID.Equals(model.ShipVia));
                if (!matchShipVia.Any()) {
                return new BuilderResponse{ ValidationMessage = "Foreign Key Violation; " + nameof(model.ShipVia) + string.Format("OrderPlural with ShipVia = '{0}' doesn't exist.", model.ShipVia)}; 
                }
            }

            OrderPlural entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("OrderPlural update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteOrderPlural(int orderID) {

            var query = Search(_entities.OrderPlurals, x => x.OrderID == orderID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("OrderPlural with _orderID = '{0}' doesn't exist.",orderID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.OrderPlurals.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("OrderPlural deleted with values: '{0}'", JsonConvert.SerializeObject(new OrderPluralModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<OrderPlural> Search(IQueryable<OrderPlural> query, Expression<Func<OrderPlural, bool>> filter) {
            return query.Where(filter);
        }
    }
}

