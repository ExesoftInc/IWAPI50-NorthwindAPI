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
    [Route("CustomerCustomerDemo")]
    public class CustomerCustomerDemoController : ControllerBase {
        
        private ICustomerCustomerDemoBuilder _builder;
        
        public CustomerCustomerDemoController(ICustomerCustomerDemoBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<IList<CustomerCustomerDemoModel>> GetCustomerCustomerDemoes() {

            return await _builder.GetCustomerCustomerDemoes()?.ToListAsync();
        }
        
        [HttpGet("Display")]
        public IList<ExpandoObject> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(CustomerCustomerDemoModel.CustomerID));
            propNames.Add(nameof(CustomerCustomerDemoModel.CustomerTypeID));

            return _builder.GetDisplayModels(propNames);
        }
        
        [HttpGet("Paged")]
        public async Task<IPagedList<CustomerCustomerDemoModel>> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetCustomerCustomerDemoes()?.ToListAsync();

            return models.ToPagedList(pageIndex, pageSize, 0, models.Count);
        }
        
        [HttpGet("{customerID}/{customerTypeID}")]
        public async Task<ActionResult> GetCustomerCustomerDemo_ByCustomerIDCustomerTypeID(string customerID, string customerTypeID) {

             var response = await _builder.GetCustomerCustomerDemo_ByCustomerIDCustomerTypeID(customerID, customerTypeID);
            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetCustomerCustomerDemo_ByCustomerID/{customerID}")]
        public async Task<IList<CustomerCustomerDemoModel>> GetCustomerCustomerDemo_ByCustomerID(string customerID) {

            return await _builder.GetCustomerCustomerDemo_ByCustomerID(customerID)?.ToListAsync();
        }
        
        [HttpGet("GetCustomerCustomerDemo_ByCustomerTypeID/{customerTypeID}")]
        public async Task<IList<CustomerCustomerDemoModel>> GetCustomerCustomerDemo_ByCustomerTypeID(string customerTypeID) {

            return await _builder.GetCustomerCustomerDemo_ByCustomerTypeID(customerTypeID)?.ToListAsync();
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddCustomerCustomerDemo([FromBody]CustomerCustomerDemoModel model) {

            if (!ModelState.IsValid) {
              return BadRequest(ModelState);
            }

            var response = await _builder.AddCustomerCustomerDemo(model);
            if (response.ErrorMessage != null) {
              return BadRequest($"Error for {nameof(AddCustomerCustomerDemo)}; {response.ErrorMessage}");
            }

            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return CreatedAtAction("GetCustomerCustomerDemo_ByCustomerIDCustomerTypeID", new {customerID = ((CustomerCustomerDemoModel)response.Model).CustomerID, customerTypeID = ((CustomerCustomerDemoModel)response.Model).CustomerTypeID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateCustomerCustomerDemo([FromBody]CustomerCustomerDemoModel model) {

            if (!ModelState.IsValid) {
              return BadRequest(ModelState);
            }
            var response = await _builder.UpdateCustomerCustomerDemo(model);
            if (response.ErrorMessage != null) {
              return BadRequest($"Error for {nameof(UpdateCustomerCustomerDemo)}; {response.ErrorMessage}");
            }

            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return StatusCode(response.StatusCode);
        }
        
        [HttpDelete("{customerID}/{customerTypeID}")]
        public async Task<ActionResult> DeleteCustomerCustomerDemo(string customerID, string customerTypeID) {

            var response = await _builder.DeleteCustomerCustomerDemo(customerID, customerTypeID);
            if (response.ErrorMessage != null) {
              return BadRequest($"Error for {nameof(DeleteCustomerCustomerDemo)}; {response.ErrorMessage}");
            }

            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}
