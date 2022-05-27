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
    
    
    public class OrderDetailPluralBuilder : IOrderDetailPluralBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public OrderDetailPluralBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<OrderDetailPlural, OrderDetailPluralModel>>  ProjectToModel {
            get {
                return entity => new OrderDetailPluralModel(entity);
            }
        }
        
        public async Task<IQueryable<OrderDetailPluralModel>> GetOrderDetailPlurals() {
            return await Task.FromResult(_entities.OrderDetailPlurals.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.OrderDetailPlurals.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetOrderDetailPlural_ByOrderIDProductID(int orderID, int productID) {
            var query = await Search(_entities.OrderDetailPlurals, x => x.OrderID == orderID&& x.ProductID == productID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; OrderDetailPlural with orderID, productID = '{orderID}', '{productID}' doesn't exist." }; 
            }
        }
        
        public async Task<IQueryable<OrderDetailPluralModel>> GetOrderDetailPlural_ByOrderID(int orderID) {

            var query = await Task.FromResult(Search(_entities.OrderDetailPlurals, x => x.OrderID == orderID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<IQueryable<OrderDetailPluralModel>> GetOrderDetailPlural_ByProductID(int productID) {

            var query = await Task.FromResult(Search(_entities.OrderDetailPlurals, x => x.ProductID == productID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddOrderDetailPlural(OrderDetailPluralModel model) {

            var matchOrderID = _entities.OrderPlurals.Where(x => x.OrderID.Equals(model.OrderID));
            if (!matchOrderID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.OrderID) + " '{model.OrderID}' doesn't exist in the system."}; 
            }

            var matchProductID = _entities.ProductPlurals.Where(x => x.ProductID.Equals(model.ProductID));
            if (!matchProductID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.ProductID) + " '{model.ProductID}' doesn't exist in the system."}; 
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.OrderDetailPlurals.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("OrderDetailPlural added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new OrderDetailPluralModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateOrderDetailPlural(OrderDetailPluralModel model) {

            var query = Search(_entities.OrderDetailPlurals, x =>  x.OrderID == model.OrderID && x.ProductID == model.ProductID);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("OrderDetailPlural with _orderID, _productID = '{0}', '{1}' doesn't exist.",model.OrderID, model.ProductID)}; 
            }

            var matchOrderID = _entities.OrderPlurals.Where(x => x.OrderID.Equals(model.OrderID));
            if (!matchOrderID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.OrderID) + string.Format("OrderID = '{0}' doesn't exist in the system.", model.OrderID)}; 
            }

            var matchProductID = _entities.ProductPlurals.Where(x => x.ProductID.Equals(model.ProductID));
            if (!matchProductID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.ProductID) + string.Format("ProductID = '{0}' doesn't exist in the system.", model.ProductID)}; 
            }

            OrderDetailPlural entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("OrderDetailPlural update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteOrderDetailPlural(int orderID, int productID) {

            var query = Search(_entities.OrderDetailPlurals, x => x.OrderID == orderID&& x.ProductID == productID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("OrderDetailPlural with _orderID, _productID = '{0}', '{1}' doesn't exist.",orderID, productID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.OrderDetailPlurals.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("OrderDetailPlural deleted with values: '{0}'", JsonConvert.SerializeObject(new OrderDetailPluralModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<OrderDetailPlural> Search(IQueryable<OrderDetailPlural> query, Expression<Func<OrderDetailPlural, bool>> filter) {
            return query.Where(filter);
        }
    }
}

