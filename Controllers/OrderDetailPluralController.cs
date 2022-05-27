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
    [Route("OrderDetailPlural")]
    public class OrderDetailPluralController : ControllerBase {
        
        private IOrderDetailPluralBuilder _builder;
        
        public OrderDetailPluralController(IOrderDetailPluralBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetOrderDetailPlurals() {

            return Ok(await _builder.GetOrderDetailPlurals());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(OrderDetailPluralModel.Quantity));
            propNames.Add(nameof(OrderDetailPluralModel.Discount));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetOrderDetailPlurals();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{orderID}/{productID}")]
        public async Task<ActionResult> GetOrderDetailPlural_ByOrderIDProductID(int orderID, int productID) {

             var response = await _builder.GetOrderDetailPlural_ByOrderIDProductID(orderID, productID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetOrderDetailPlural_ByOrderID/{orderID}")]
        public async Task<IQueryable<OrderDetailPluralModel>> GetOrderDetailPlural_ByOrderID(int orderID) {

            return await _builder.GetOrderDetailPlural_ByOrderID(orderID);
        }
        
        [HttpGet("GetOrderDetailPlural_ByProductID/{productID}")]
        public async Task<IQueryable<OrderDetailPluralModel>> GetOrderDetailPlural_ByProductID(int productID) {

            return await _builder.GetOrderDetailPlural_ByProductID(productID);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddOrderDetailPlural([FromBody]OrderDetailPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddOrderDetailPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetOrderDetailPlural_ByOrderIDProductID", new {orderID = ((OrderDetailPluralModel)response.Model).OrderID, productID = ((OrderDetailPluralModel)response.Model).ProductID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateOrderDetailPlural([FromBody]OrderDetailPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateOrderDetailPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetOrderDetailPlural_ByOrderIDProductID", new {orderID = model.OrderID, productID = model.ProductID}, model);
        }
        
        [HttpDelete("{orderID}/{productID}")]
        public async Task<ActionResult> DeleteOrderDetailPlural(int orderID, int productID) {

            var response = await _builder.DeleteOrderDetailPlural(orderID, productID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

