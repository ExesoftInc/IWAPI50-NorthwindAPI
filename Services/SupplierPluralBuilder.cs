// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using AutoMapper;
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
        
        private IMapper _mapper;
        
        private ILoggerManager _logger;
        
        public SupplierPluralBuilder(EntitiesContext context, IMapper mapper, ILoggerManager logger) {
            _entities = context;
            _mapper = mapper;
            _logger = logger;
        }
        
        private Expression<Func<SupplierPlural, SupplierPluralModel>>  ProjectToModel {
            get {
                return entity => _mapper.Map<SupplierPluralModel>(entity);
            }
        }
        
        public IQueryable<SupplierPluralModel> GetSupplierPlurals() {
            return _entities.SupplierPlurals.Select(ProjectToModel);
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
           return new BuilderResponse { RequestMessage = $"Record Not Found; SupplierPlural with supplierID = '{supplierID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddSupplierPlural(SupplierPluralModel model) {
           try
           {
                 var entity = _mapper.Map<SupplierPlural>(model);
                _entities.SupplierPlurals.Add(entity);
               await _entities.SaveChangesAsync();
                _logger.LogInfo(string.Format("SupplierPlural added with values: '{0}'", JsonConvert.SerializeObject(model)));
               return new BuilderResponse{ Model = new SupplierPluralModel(entity) }; 
            }
            catch (DbUpdateException ue)
            {
                if(ue.InnerException != null && ue.InnerException.Message.Contains("Cannot insert explicit value for identity column"))
                {
                    var inner = ue.InnerException;
                    _logger.LogError(inner.Message + Environment.NewLine + JsonConvert.SerializeObject(model) + Environment.NewLine + inner.StackTrace);
                    return new BuilderResponse { ErrorMessage = "IDENTITY_INSERT is set to OFF; Cannot insert explicit value for identity column when IDENTITY_INSERT is set to OFF."};
                }
                else if(ue.InnerException != null && ue.InnerException.Message.Contains("Cannot insert duplicate key row"))
                {
                    var inner = ue.InnerException;
                    _logger.LogError(inner.Message + Environment.NewLine + JsonConvert.SerializeObject(model) + Environment.NewLine + inner.StackTrace);
                    return new BuilderResponse { ErrorMessage = "Duplicate exception; Please verify that an item with these values doesn't already exists."};
                }
                _logger.LogError(ue.Message + ue.StackTrace);
                return new BuilderResponse { ErrorMessage = ue.Message };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + e.StackTrace);
                return new BuilderResponse { ErrorMessage = e.Message};
            }
        }
        
        public async Task<BuilderResponse> UpdateSupplierPlural(SupplierPluralModel model) {

          var query = Search(_entities.SupplierPlurals, x =>  x.SupplierID == model.SupplierID);
            if (!query.Any()) {
              return new BuilderResponse { RequestMessage = "Record Not Found; " + string.Format("SupplierPlural with _supplierID = '{0}' doesn't exist.",model.SupplierID)}; 
            }
           try
           {
            SupplierPlural entity = query.SingleOrDefault();
             entity = model.ToEntity(entity);
               await _entities.SaveChangesAsync();
                _logger.LogInfo(string.Format("SupplierPlural update with values: '{0}'", JsonConvert.SerializeObject(model)));
               return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Created }; 
            }
            catch (DbUpdateException ue)
            {
                if(ue.InnerException != null && ue.InnerException.Message.Contains("Cannot insert duplicate key row"))
                {
                    var inner = ue.InnerException;
                    _logger.LogError(inner.Message + Environment.NewLine + JsonConvert.SerializeObject(model) + Environment.NewLine + inner.StackTrace);
                    return new BuilderResponse { ErrorMessage = "Duplicate exception; Please verify that an item with these values doesn't already exists."};
                }
                _logger.LogError(ue.Message + ue.StackTrace);
                return new BuilderResponse { ErrorMessage = ue.Message };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + e.StackTrace);
                return new BuilderResponse { ErrorMessage = e.Message};
            }
        }
        
        public async Task<BuilderResponse> DeleteSupplierPlural(int supplierID) {
          var query = Search(_entities.SupplierPlurals, x => x.SupplierID == supplierID);
            if (!query.Any()) {
              return new BuilderResponse { RequestMessage = "Record Not Found; " + string.Format("SupplierPlural with _supplierID = '{0}' doesn't exist.",supplierID)}; 
            }
            var entity = query.SingleOrDefault();

           try
           {
                _entities.SupplierPlurals.Remove(entity);
               await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("SupplierPlural deleted with values: '{0}'", JsonConvert.SerializeObject(new SupplierPluralModel(entity))));
               return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
            }
            catch (DbUpdateException ue)
            {
                if(ue.InnerException != null && ue.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    var inner = ue.InnerException;
                    _logger.LogError(inner.Message + inner.StackTrace);
                    return new BuilderResponse { ErrorMessage = "Please delete related items first."};
                }
                _logger.LogError(ue.Message + ue.StackTrace);
                return new BuilderResponse { ErrorMessage = ue.Message };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + e.StackTrace);
                return new BuilderResponse { ErrorMessage = e.Message};
            }
        }
        
        private IQueryable<SupplierPlural> Search(IQueryable<SupplierPlural> query, Expression<Func<SupplierPlural, bool>> filter) {
            return query.Where(filter);
        }
    }
}
