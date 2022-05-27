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
    
    
    public class ShipperPluralBuilder : IShipperPluralBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public ShipperPluralBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<ShipperPlural, ShipperPluralModel>>  ProjectToModel {
            get {
                return entity => new ShipperPluralModel(entity);
            }
        }
        
        public async Task<IQueryable<ShipperPluralModel>> GetShipperPlurals() {
            return await Task.FromResult(_entities.ShipperPlurals.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.ShipperPlurals.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetShipperPlural_ByShipperID(int shipperID) {
            var query = await Search(_entities.ShipperPlurals, x => x.ShipperID == shipperID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; ShipperPlural with shipperID = '{shipperID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddShipperPlural(ShipperPluralModel model) {


            var entity = ModelExtender.ToEntity(model);
            _entities.ShipperPlurals.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("ShipperPlural added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new ShipperPluralModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateShipperPlural(ShipperPluralModel model) {

            var query = Search(_entities.ShipperPlurals, x =>  x.ShipperID == model.ShipperID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("ShipperPlural with _shipperID = '{0}' doesn't exist.",model.ShipperID)}; 
            }

            ShipperPlural entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("ShipperPlural update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteShipperPlural(int shipperID) {

            var query = Search(_entities.ShipperPlurals, x => x.ShipperID == shipperID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("ShipperPlural with _shipperID = '{0}' doesn't exist.",shipperID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.ShipperPlurals.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("ShipperPlural deleted with values: '{0}'", JsonConvert.SerializeObject(new ShipperPluralModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<ShipperPlural> Search(IQueryable<ShipperPlural> query, Expression<Func<ShipperPlural, bool>> filter) {
            return query.Where(filter);
        }
    }
}

