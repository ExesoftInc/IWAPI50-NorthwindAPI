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
    [Route("OrderPlural")]
    public class OrderPluralController : ControllerBase {
        
        private IOrderPluralBuilder _builder;
        
        public OrderPluralController(IOrderPluralBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetOrderPlurals() {

            return Ok(await _builder.GetOrderPlurals());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(OrderPluralModel.OrderID));
            propNames.Add(nameof(OrderPluralModel.CustomerID));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetOrderPlurals();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{orderID}")]
        public async Task<ActionResult> GetOrderPlural_ByOrderID(int orderID) {

             var response = await _builder.GetOrderPlural_ByOrderID(orderID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetOrderPlural_ByCustomerID/{customerID}")]
        public async Task<IQueryable<OrderPluralModel>> GetOrderPlural_ByCustomerID(string customerID) {

            return await _builder.GetOrderPlural_ByCustomerID(customerID);
        }
        
        [HttpGet("GetOrderPlural_ByEmployeeID/{employeeID}")]
        public async Task<IQueryable<OrderPluralModel>> GetOrderPlural_ByEmployeeID(int employeeID) {

            return await _builder.GetOrderPlural_ByEmployeeID(employeeID);
        }
        
        [HttpGet("GetOrderPlural_ByShipVia/{shipVia}")]
        public async Task<IQueryable<OrderPluralModel>> GetOrderPlural_ByShipVia(int shipVia) {

            return await _builder.GetOrderPlural_ByShipVia(shipVia);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddOrderPlural([FromBody]OrderPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddOrderPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetOrderPlural_ByOrderID", new {orderID = ((OrderPluralModel)response.Model).OrderID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateOrderPlural([FromBody]OrderPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateOrderPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetOrderPlural_ByOrderID", new {orderID = model.OrderID}, model);
        }
        
        [HttpDelete("{orderID}")]
        public async Task<ActionResult> DeleteOrderPlural(int orderID) {

            var response = await _builder.DeleteOrderPlural(orderID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

