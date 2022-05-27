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
    
    
    public class SupplierPluralBuilder : ISupplierPluralBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public SupplierPluralBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<SupplierPlural, SupplierPluralModel>>  ProjectToModel {
            get {
                return entity => new SupplierPluralModel(entity);
            }
        }
        
        public async Task<IQueryable<SupplierPluralModel>> GetSupplierPlurals() {
            return await Task.FromResult(_entities.SupplierPlurals.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.SupplierPlurals.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetSupplierPlural_BySupplierID(int supplierID) {
            var query = await Search(_entities.SupplierPlurals, x => x.SupplierID == supplierID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; SupplierPlural with supplierID = '{supplierID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddSupplierPlural(SupplierPluralModel model) {


            var entity = ModelExtender.ToEntity(model);
            _entities.SupplierPlurals.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("SupplierPlural added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new SupplierPluralModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateSupplierPlural(SupplierPluralModel model) {

            var query = Search(_entities.SupplierPlurals, x =>  x.SupplierID == model.SupplierID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("SupplierPlural with _supplierID = '{0}' doesn't exist.",model.SupplierID)}; 
            }

            SupplierPlural entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("SupplierPlural update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteSupplierPlural(int supplierID) {

            var query = Search(_entities.SupplierPlurals, x => x.SupplierID == supplierID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("SupplierPlural with _supplierID = '{0}' doesn't exist.",supplierID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.SupplierPlurals.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("SupplierPlural deleted with values: '{0}'", JsonConvert.SerializeObject(new SupplierPluralModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<SupplierPlural> Search(IQueryable<SupplierPlural> query, Expression<Func<SupplierPlural, bool>> filter) {
            return query.Where(filter);
        }
    }
}

