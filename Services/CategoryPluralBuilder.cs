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
    
    
    public class CategoryPluralBuilder : ICategoryPluralBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public CategoryPluralBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<CategoryPlural, CategoryPluralModel>>  ProjectToModel {
            get {
                return entity => new CategoryPluralModel(entity);
            }
        }
        
        public async Task<IQueryable<CategoryPluralModel>> GetCategoryPlurals() {
            return await Task.FromResult(_entities.CategoryPlurals.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.CategoryPlurals.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetCategoryPlural_ByCategoryID(int categoryID) {
            var query = await Search(_entities.CategoryPlurals, x => x.CategoryID == categoryID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; CategoryPlural with categoryID = '{categoryID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddCategoryPlural(CategoryPluralModel model) {


            var entity = ModelExtender.ToEntity(model);
            _entities.CategoryPlurals.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CategoryPlural added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new CategoryPluralModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateCategoryPlural(CategoryPluralModel model) {

            var query = Search(_entities.CategoryPlurals, x =>  x.CategoryID == model.CategoryID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("CategoryPlural with _categoryID = '{0}' doesn't exist.",model.CategoryID)}; 
            }

            CategoryPlural entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CategoryPlural update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteCategoryPlural(int categoryID) {

            var query = Search(_entities.CategoryPlurals, x => x.CategoryID == categoryID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("CategoryPlural with _categoryID = '{0}' doesn't exist.",categoryID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.CategoryPlurals.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CategoryPlural deleted with values: '{0}'", JsonConvert.SerializeObject(new CategoryPluralModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<CategoryPlural> Search(IQueryable<CategoryPlural> query, Expression<Func<CategoryPlural, bool>> filter) {
            return query.Where(filter);
        }
    }
}

