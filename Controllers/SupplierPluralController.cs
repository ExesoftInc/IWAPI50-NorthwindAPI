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
    [Route("SupplierPlural")]
    public class SupplierPluralController : ControllerBase {
        
        private ISupplierPluralBuilder _builder;
        
        public SupplierPluralController(ISupplierPluralBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetSupplierPlurals() {

            return Ok(await _builder.GetSupplierPlurals());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(SupplierPluralModel.SupplierID));
            propNames.Add(nameof(SupplierPluralModel.CompanyName));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetSupplierPlurals();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{supplierID}")]
        public async Task<ActionResult> GetSupplierPlural_BySupplierID(int supplierID) {

             var response = await _builder.GetSupplierPlural_BySupplierID(supplierID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddSupplierPlural([FromBody]SupplierPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddSupplierPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetSupplierPlural_BySupplierID", new {supplierID = ((SupplierPluralModel)response.Model).SupplierID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateSupplierPlural([FromBody]SupplierPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateSupplierPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetSupplierPlural_BySupplierID", new {supplierID = model.SupplierID}, model);
        }
        
        [HttpDelete("{supplierID}")]
        public async Task<ActionResult> DeleteSupplierPlural(int supplierID) {

            var response = await _builder.DeleteSupplierPlural(supplierID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

