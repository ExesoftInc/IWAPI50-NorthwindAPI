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
    
    
    public class ShipperPluralBuilder : IShipperPluralBuilder {
        
        private IDbEntities _entities;
        
        private IMapper _mapper;
        
        private ILoggerManager _logger;
        
        public ShipperPluralBuilder(EntitiesContext context, IMapper mapper, ILoggerManager logger) {
            _entities = context;
            _mapper = mapper;
            _logger = logger;
        }
        
        private Expression<Func<ShipperPlural, ShipperPluralModel>>  ProjectToModel {
            get {
                return entity => _mapper.Map<ShipperPluralModel>(entity);
            }
        }
        
        public IQueryable<ShipperPluralModel> GetShipperPlurals() {
            return _entities.ShipperPlurals.Select(ProjectToModel);
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
           return new BuilderResponse { RequestMessage = $"Record Not Found; ShipperPlural with shipperID = '{shipperID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddShipperPlural(ShipperPluralModel model) {
           try
           {
                 var entity = _mapper.Map<ShipperPlural>(model);
                _entities.ShipperPlurals.Add(entity);
               await _entities.SaveChangesAsync();
                _logger.LogInfo(string.Format("ShipperPlural added with values: '{0}'", JsonConvert.SerializeObject(model)));
               return new BuilderResponse{ Model = new ShipperPluralModel(entity) }; 
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
        
        public async Task<BuilderResponse> UpdateShipperPlural(ShipperPluralModel model) {

          var query = Search(_entities.ShipperPlurals, x =>  x.ShipperID == model.ShipperID);
            if (!query.Any()) {
              return new BuilderResponse { RequestMessage = "Record Not Found; " + string.Format("ShipperPlural with _shipperID = '{0}' doesn't exist.",model.ShipperID)}; 
            }
           try
           {
            ShipperPlural entity = query.SingleOrDefault();
             entity = model.ToEntity(entity);
               await _entities.SaveChangesAsync();
                _logger.LogInfo(string.Format("ShipperPlural update with values: '{0}'", JsonConvert.SerializeObject(model)));
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
        
        public async Task<BuilderResponse> DeleteShipperPlural(int shipperID) {
          var query = Search(_entities.ShipperPlurals, x => x.ShipperID == shipperID);
            if (!query.Any()) {
              return new BuilderResponse { RequestMessage = "Record Not Found; " + string.Format("ShipperPlural with _shipperID = '{0}' doesn't exist.",shipperID)}; 
            }
            var entity = query.SingleOrDefault();

           try
           {
                _entities.ShipperPlurals.Remove(entity);
               await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("ShipperPlural deleted with values: '{0}'", JsonConvert.SerializeObject(new ShipperPluralModel(entity))));
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
        
        private IQueryable<ShipperPlural> Search(IQueryable<ShipperPlural> query, Expression<Func<ShipperPlural, bool>> filter) {
            return query.Where(filter);
        }
    }
}
