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
    [Route("CustomerDemographicPlural")]
    public class CustomerDemographicPluralController : ControllerBase {
        
        private ICustomerDemographicPluralBuilder _builder;
        
        public CustomerDemographicPluralController(ICustomerDemographicPluralBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetCustomerDemographicPlurals() {

            return Ok(await _builder.GetCustomerDemographicPlurals());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(CustomerDemographicPluralModel.CustomerTypeID));
            propNames.Add(nameof(CustomerDemographicPluralModel.CustomerDesc));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetCustomerDemographicPlurals();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{customerTypeID}")]
        public async Task<ActionResult> GetCustomerDemographicPlural_ByCustomerTypeID(string customerTypeID) {

             var response = await _builder.GetCustomerDemographicPlural_ByCustomerTypeID(customerTypeID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddCustomerDemographicPlural([FromBody]CustomerDemographicPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddCustomerDemographicPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetCustomerDemographicPlural_ByCustomerTypeID", new {customerTypeID = ((CustomerDemographicPluralModel)response.Model).CustomerTypeID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateCustomerDemographicPlural([FromBody]CustomerDemographicPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateCustomerDemographicPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetCustomerDemographicPlural_ByCustomerTypeID", new {customerTypeID = model.CustomerTypeID}, model);
        }
        
        [HttpDelete("{customerTypeID}")]
        public async Task<ActionResult> DeleteCustomerDemographicPlural(string customerTypeID) {

            var response = await _builder.DeleteCustomerDemographicPlural(customerTypeID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

