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
    [Route("CustomerCustomerDemo")]
    public class CustomerCustomerDemoController : ControllerBase {
        
        private ICustomerCustomerDemoBuilder _builder;
        
        public CustomerCustomerDemoController(ICustomerCustomerDemoBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetCustomerCustomerDemoes() {

            return Ok(await _builder.GetCustomerCustomerDemoes());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(CustomerCustomerDemoModel.CustomerID));
            propNames.Add(nameof(CustomerCustomerDemoModel.CustomerTypeID));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetCustomerCustomerDemoes();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{customerID}/{customerTypeID}")]
        public async Task<ActionResult> GetCustomerCustomerDemo_ByCustomerIDCustomerTypeID(string customerID, string customerTypeID) {

             var response = await _builder.GetCustomerCustomerDemo_ByCustomerIDCustomerTypeID(customerID, customerTypeID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetCustomerCustomerDemo_ByCustomerID/{customerID}")]
        public async Task<IQueryable<CustomerCustomerDemoModel>> GetCustomerCustomerDemo_ByCustomerID(string customerID) {

            return await _builder.GetCustomerCustomerDemo_ByCustomerID(customerID);
        }
        
        [HttpGet("GetCustomerCustomerDemo_ByCustomerTypeID/{customerTypeID}")]
        public async Task<IQueryable<CustomerCustomerDemoModel>> GetCustomerCustomerDemo_ByCustomerTypeID(string customerTypeID) {

            return await _builder.GetCustomerCustomerDemo_ByCustomerTypeID(customerTypeID);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddCustomerCustomerDemo([FromBody]CustomerCustomerDemoModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddCustomerCustomerDemo(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetCustomerCustomerDemo_ByCustomerIDCustomerTypeID", new {customerID = ((CustomerCustomerDemoModel)response.Model).CustomerID, customerTypeID = ((CustomerCustomerDemoModel)response.Model).CustomerTypeID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateCustomerCustomerDemo([FromBody]CustomerCustomerDemoModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateCustomerCustomerDemo(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetCustomerCustomerDemo_ByCustomerIDCustomerTypeID", new {customerID = model.CustomerID, customerTypeID = model.CustomerTypeID}, model);
        }
        
        [HttpDelete("{customerID}/{customerTypeID}")]
        public async Task<ActionResult> DeleteCustomerCustomerDemo(string customerID, string customerTypeID) {

            var response = await _builder.DeleteCustomerCustomerDemo(customerID, customerTypeID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

