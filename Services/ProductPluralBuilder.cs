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
    
    
    public class ProductPluralBuilder : IProductPluralBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public ProductPluralBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<ProductPlural, ProductPluralModel>>  ProjectToModel {
            get {
                return entity => new ProductPluralModel(entity);
            }
        }
        
        public async Task<IQueryable<ProductPluralModel>> GetProductPlurals() {
            return await Task.FromResult(_entities.ProductPlurals.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.ProductPlurals.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetProductPlural_ByProductID(int productID) {
            var query = await Search(_entities.ProductPlurals, x => x.ProductID == productID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; ProductPlural with productID = '{productID}' doesn't exist." }; 
            }
        }
        
        public async Task<IQueryable<ProductPluralModel>> GetProductPlural_BySupplierID(int supplierID) {

            var query = await Task.FromResult(Search(_entities.ProductPlurals, x => x.SupplierID == supplierID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<IQueryable<ProductPluralModel>> GetProductPlural_ByCategoryID(int categoryID) {

            var query = await Task.FromResult(Search(_entities.ProductPlurals, x => x.CategoryID == categoryID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddProductPlural(ProductPluralModel model) {

            if (model.SupplierID != null) {
                var matchSupplierID = _entities.SupplierPlurals.Where(x => x.SupplierID.Equals(model.SupplierID));
                if (!matchSupplierID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.SupplierID) + " '{model.SupplierID}' doesn't exist in the system."}; 
                }
            }

            if (model.CategoryID != null) {
                var matchCategoryID = _entities.CategoryPlurals.Where(x => x.CategoryID.Equals(model.CategoryID));
                if (!matchCategoryID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.CategoryID) + " '{model.CategoryID}' doesn't exist in the system."}; 
                }
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.ProductPlurals.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("ProductPlural added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new ProductPluralModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateProductPlural(ProductPluralModel model) {

            var query = Search(_entities.ProductPlurals, x =>  x.ProductID == model.ProductID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("ProductPlural with _productID = '{0}' doesn't exist.",model.ProductID)}; 
            }

            if (model.SupplierID != null) {
                var matchSupplierID = _entities.SupplierPlurals.Where(x => x.SupplierID.Equals(model.SupplierID));
                if (!matchSupplierID.Any()) {
                return new BuilderResponse{ ValidationMessage = "Foreign Key Violation; " + nameof(model.SupplierID) + string.Format("ProductPlural with SupplierID = '{0}' doesn't exist.", model.SupplierID)}; 
                }
            }

            if (model.CategoryID != null) {
                var matchCategoryID = _entities.CategoryPlurals.Where(x => x.CategoryID.Equals(model.CategoryID));
                if (!matchCategoryID.Any()) {
                return new BuilderResponse{ ValidationMessage = "Foreign Key Violation; " + nameof(model.CategoryID) + string.Format("ProductPlural with CategoryID = '{0}' doesn't exist.", model.CategoryID)}; 
                }
            }

            ProductPlural entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("ProductPlural update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteProductPlural(int productID) {

            var query = Search(_entities.ProductPlurals, x => x.ProductID == productID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("ProductPlural with _productID = '{0}' doesn't exist.",productID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.ProductPlurals.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("ProductPlural deleted with values: '{0}'", JsonConvert.SerializeObject(new ProductPluralModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<ProductPlural> Search(IQueryable<ProductPlural> query, Expression<Func<ProductPlural, bool>> filter) {
            return query.Where(filter);
        }
    }
}

