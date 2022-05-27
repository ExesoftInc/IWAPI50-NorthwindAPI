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
    [Route("Region")]
    public class RegionController : ControllerBase {
        
        private IRegionBuilder _builder;
        
        public RegionController(IRegionBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetRegions() {

            return Ok(await _builder.GetRegions());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(RegionModel.RegionID));
            propNames.Add(nameof(RegionModel.RegionDescription));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetRegions();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{regionID}")]
        public async Task<ActionResult> GetRegion_ByRegionID(int regionID) {

             var response = await _builder.GetRegion_ByRegionID(regionID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddRegion([FromBody]RegionModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddRegion(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetRegion_ByRegionID", new {regionID = ((RegionModel)response.Model).RegionID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateRegion([FromBody]RegionModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateRegion(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetRegion_ByRegionID", new {regionID = model.RegionID}, model);
        }
        
        [HttpDelete("{regionID}")]
        public async Task<ActionResult> DeleteRegion(int regionID) {

            var response = await _builder.DeleteRegion(regionID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

