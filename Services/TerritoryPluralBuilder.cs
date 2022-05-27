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
    
    
    public class TerritoryPluralBuilder : ITerritoryPluralBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public TerritoryPluralBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<TerritoryPlural, TerritoryPluralModel>>  ProjectToModel {
            get {
                return entity => new TerritoryPluralModel(entity);
            }
        }
        
        public async Task<IQueryable<TerritoryPluralModel>> GetTerritoryPlurals() {
            return await Task.FromResult(_entities.TerritoryPlurals.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.TerritoryPlurals.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetTerritoryPlural_ByTerritoryID(string territoryID) {
            var query = await Search(_entities.TerritoryPlurals, x => x.TerritoryID == territoryID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; TerritoryPlural with territoryID = '{territoryID}' doesn't exist." }; 
            }
        }
        
        public async Task<IQueryable<TerritoryPluralModel>> GetTerritoryPlural_ByRegionID(int regionID) {

            var query = await Task.FromResult(Search(_entities.TerritoryPlurals, x => x.RegionID == regionID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddTerritoryPlural(TerritoryPluralModel model) {

            var matchRegionID = _entities.Regions.Where(x => x.RegionID.Equals(model.RegionID));
            if (!matchRegionID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.RegionID) + " '{model.RegionID}' doesn't exist in the system."}; 
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.TerritoryPlurals.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("TerritoryPlural added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new TerritoryPluralModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateTerritoryPlural(TerritoryPluralModel model) {

            var query = Search(_entities.TerritoryPlurals, x =>  x.TerritoryID == model.TerritoryID);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("TerritoryPlural with _territoryID = '{0}' doesn't exist.",model.TerritoryID)}; 
            }

            var matchRegionID = _entities.Regions.Where(x => x.RegionID.Equals(model.RegionID));
            if (!matchRegionID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.RegionID) + string.Format("RegionID = '{0}' doesn't exist in the system.", model.RegionID)}; 
            }

            TerritoryPlural entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("TerritoryPlural update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteTerritoryPlural(string territoryID) {

            var query = Search(_entities.TerritoryPlurals, x => x.TerritoryID == territoryID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("TerritoryPlural with _territoryID = '{0}' doesn't exist.",territoryID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.TerritoryPlurals.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("TerritoryPlural deleted with values: '{0}'", JsonConvert.SerializeObject(new TerritoryPluralModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<TerritoryPlural> Search(IQueryable<TerritoryPlural> query, Expression<Func<TerritoryPlural, bool>> filter) {
            return query.Where(filter);
        }
    }
}

