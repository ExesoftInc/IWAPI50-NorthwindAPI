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
    [Route("CustomerPlural")]
    public class CustomerPluralController : ControllerBase {
        
        private ICustomerPluralBuilder _builder;
        
        public CustomerPluralController(ICustomerPluralBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetCustomerPlurals() {

            return Ok(await _builder.GetCustomerPlurals());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(CustomerPluralModel.CustomerID));
            propNames.Add(nameof(CustomerPluralModel.CompanyName));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetCustomerPlurals();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{customerID}")]
        public async Task<ActionResult> GetCustomerPlural_ByCustomerID(string customerID) {

             var response = await _builder.GetCustomerPlural_ByCustomerID(customerID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddCustomerPlural([FromBody]CustomerPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddCustomerPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetCustomerPlural_ByCustomerID", new {customerID = ((CustomerPluralModel)response.Model).CustomerID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateCustomerPlural([FromBody]CustomerPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateCustomerPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetCustomerPlural_ByCustomerID", new {customerID = model.CustomerID}, model);
        }
        
        [HttpDelete("{customerID}")]
        public async Task<ActionResult> DeleteCustomerPlural(string customerID) {

            var response = await _builder.DeleteCustomerPlural(customerID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

