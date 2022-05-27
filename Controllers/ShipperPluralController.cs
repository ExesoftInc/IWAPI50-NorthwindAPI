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
    [Route("ShipperPlural")]
    public class ShipperPluralController : ControllerBase {
        
        private IShipperPluralBuilder _builder;
        
        public ShipperPluralController(IShipperPluralBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetShipperPlurals() {

            return Ok(await _builder.GetShipperPlurals());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(ShipperPluralModel.ShipperID));
            propNames.Add(nameof(ShipperPluralModel.CompanyName));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetShipperPlurals();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{shipperID}")]
        public async Task<ActionResult> GetShipperPlural_ByShipperID(int shipperID) {

             var response = await _builder.GetShipperPlural_ByShipperID(shipperID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddShipperPlural([FromBody]ShipperPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddShipperPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetShipperPlural_ByShipperID", new {shipperID = ((ShipperPluralModel)response.Model).ShipperID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateShipperPlural([FromBody]ShipperPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateShipperPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetShipperPlural_ByShipperID", new {shipperID = model.ShipperID}, model);
        }
        
        [HttpDelete("{shipperID}")]
        public async Task<ActionResult> DeleteShipperPlural(int shipperID) {

            var response = await _builder.DeleteShipperPlural(shipperID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

