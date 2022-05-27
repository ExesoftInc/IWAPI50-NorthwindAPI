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
    [Route("ProductPlural")]
    public class ProductPluralController : ControllerBase {
        
        private IProductPluralBuilder _builder;
        
        public ProductPluralController(IProductPluralBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetProductPlurals() {

            return Ok(await _builder.GetProductPlurals());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(ProductPluralModel.ProductID));
            propNames.Add(nameof(ProductPluralModel.ProductName));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetProductPlurals();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{productID}")]
        public async Task<ActionResult> GetProductPlural_ByProductID(int productID) {

             var response = await _builder.GetProductPlural_ByProductID(productID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetProductPlural_BySupplierID/{supplierID}")]
        public async Task<IQueryable<ProductPluralModel>> GetProductPlural_BySupplierID(int supplierID) {

            return await _builder.GetProductPlural_BySupplierID(supplierID);
        }
        
        [HttpGet("GetProductPlural_ByCategoryID/{categoryID}")]
        public async Task<IQueryable<ProductPluralModel>> GetProductPlural_ByCategoryID(int categoryID) {

            return await _builder.GetProductPlural_ByCategoryID(categoryID);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddProductPlural([FromBody]ProductPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddProductPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetProductPlural_ByProductID", new {productID = ((ProductPluralModel)response.Model).ProductID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateProductPlural([FromBody]ProductPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateProductPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetProductPlural_ByProductID", new {productID = model.ProductID}, model);
        }
        
        [HttpDelete("{productID}")]
        public async Task<ActionResult> DeleteProductPlural(int productID) {

            var response = await _builder.DeleteProductPlural(productID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

