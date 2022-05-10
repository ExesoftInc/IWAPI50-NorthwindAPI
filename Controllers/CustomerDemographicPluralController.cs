// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using InstantHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NorthwindAPI.Models;
using NorthwindAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;

namespace NorthwindAPI.Controllers {
    
    
    // Uncomment the following line to use an API Key; change the value of the key in appSetting (X-API-Key)
    // [ApiKey()]
    [Route("CustomerDemographicPlural")]
    public class CustomerDemographicPluralController : ControllerBase {
        
        private ICustomerDemographicPluralBuilder _builder;
        
        public CustomerDemographicPluralController(ICustomerDemographicPluralBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<IList<CustomerDemographicPluralModel>> GetCustomerDemographicPlurals() {

            return await _builder.GetCustomerDemographicPlurals()?.ToListAsync();
        }
        
        [HttpGet("Display")]
        public IList<ExpandoObject> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(CustomerDemographicPluralModel.CustomerTypeID));
            propNames.Add(nameof(CustomerDemographicPluralModel.CustomerDesc));

            return _builder.GetDisplayModels(propNames);
        }
        
        [HttpGet("Paged")]
        public async Task<IPagedList<CustomerDemographicPluralModel>> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetCustomerDemographicPlurals()?.ToListAsync();

            return models.ToPagedList(pageIndex, pageSize, 0, models.Count);
        }
        
        [HttpGet("{customerTypeID}")]
        public async Task<ActionResult> GetCustomerDemographicPlural_ByCustomerTypeID(string customerTypeID) {

             var response = await _builder.GetCustomerDemographicPlural_ByCustomerTypeID(customerTypeID);
            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddCustomerDemographicPlural([FromBody]CustomerDemographicPluralModel model) {

            if (!ModelState.IsValid) {
              return BadRequest(ModelState);
            }

            var response = await _builder.AddCustomerDemographicPlural(model);
            if (response.ErrorMessage != null) {
              return BadRequest($"Error for {nameof(AddCustomerDemographicPlural)}; {response.ErrorMessage}");
            }

            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return CreatedAtAction("GetCustomerDemographicPlural_ByCustomerTypeID", new {customerTypeID = ((CustomerDemographicPluralModel)response.Model).CustomerTypeID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateCustomerDemographicPlural([FromBody]CustomerDemographicPluralModel model) {

            if (!ModelState.IsValid) {
              return BadRequest(ModelState);
            }
            var response = await _builder.UpdateCustomerDemographicPlural(model);
            if (response.ErrorMessage != null) {
              return BadRequest($"Error for {nameof(UpdateCustomerDemographicPlural)}; {response.ErrorMessage}");
            }

            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return StatusCode(response.StatusCode);
        }
        
        [HttpDelete("{customerTypeID}")]
        public async Task<ActionResult> DeleteCustomerDemographicPlural(string customerTypeID) {

            var response = await _builder.DeleteCustomerDemographicPlural(customerTypeID);
            if (response.ErrorMessage != null) {
              return BadRequest($"Error for {nameof(DeleteCustomerDemographicPlural)}; {response.ErrorMessage}");
            }

            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

