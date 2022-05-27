using InstantHelper;
using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Models;
using NorthwindAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAPI.Controllers {
    
    
    // TODO: Uncomment the following line to use an API Key; change the value of the key in appSetting (X-API-Key)
    // [ApiKey()]
    [Route("TerritoryPlural")]
    public class TerritoryPluralController : ControllerBase {
        
        private ITerritoryPluralBuilder _builder;
        
        public TerritoryPluralController(ITerritoryPluralBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetTerritoryPlurals() {

            return Ok(await _builder.GetTerritoryPlurals());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(TerritoryPluralModel.TerritoryID));
            propNames.Add(nameof(TerritoryPluralModel.TerritoryDescription));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetTerritoryPlurals();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{territoryID}")]
        public async Task<ActionResult> GetTerritoryPlural_ByTerritoryID(string territoryID) {

             var response = await _builder.GetTerritoryPlural_ByTerritoryID(territoryID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetTerritoryPlural_ByRegionID/{regionID}")]
        public async Task<IQueryable<TerritoryPluralModel>> GetTerritoryPlural_ByRegionID(int regionID) {

            return await _builder.GetTerritoryPlural_ByRegionID(regionID);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddTerritoryPlural([FromBody]TerritoryPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddTerritoryPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetTerritoryPlural_ByTerritoryID", new {territoryID = ((TerritoryPluralModel)response.Model).TerritoryID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateTerritoryPlural([FromBody]TerritoryPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateTerritoryPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetTerritoryPlural_ByTerritoryID", new {territoryID = model.TerritoryID}, model);
        }
        
        [HttpDelete("{territoryID}")]
        public async Task<ActionResult> DeleteTerritoryPlural(string territoryID) {

            var response = await _builder.DeleteTerritoryPlural(territoryID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

