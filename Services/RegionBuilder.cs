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
    
    
    public class RegionBuilder : IRegionBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public RegionBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<Region, RegionModel>>  ProjectToModel {
            get {
                return entity => new RegionModel(entity);
            }
        }
        
        public async Task<IQueryable<RegionModel>> GetRegions() {
            return await Task.FromResult(_entities.Regions.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.Regions.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetRegion_ByRegionID(int regionID) {
            var query = await Search(_entities.Regions, x => x.RegionID == regionID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; Region with regionID = '{regionID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddRegion(RegionModel model) {

            System.Int32 maxCount = 0;
            if(_entities.Regions.Count() > 0)
            maxCount = _entities.Regions.Max(x => x.RegionID);
            model.RegionID= ++maxCount;

            var entity = ModelExtender.ToEntity(model);
            _entities.Regions.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Region added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new RegionModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateRegion(RegionModel model) {

            var query = Search(_entities.Regions, x =>  x.RegionID == model.RegionID);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("Region with _regionID = '{0}' doesn't exist.",model.RegionID)}; 
            }

            Region entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Region update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteRegion(int regionID) {

            var query = Search(_entities.Regions, x => x.RegionID == regionID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("Region with _regionID = '{0}' doesn't exist.",regionID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.Regions.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Region deleted with values: '{0}'", JsonConvert.SerializeObject(new RegionModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<Region> Search(IQueryable<Region> query, Expression<Func<Region, bool>> filter) {
            return query.Where(filter);
        }
    }
}

