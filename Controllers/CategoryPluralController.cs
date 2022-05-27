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
    [Route("CategoryPlural")]
    public class CategoryPluralController : ControllerBase {
        
        private ICategoryPluralBuilder _builder;
        
        public CategoryPluralController(ICategoryPluralBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetCategoryPlurals() {

            return Ok(await _builder.GetCategoryPlurals());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(CategoryPluralModel.CategoryID));
            propNames.Add(nameof(CategoryPluralModel.CategoryName));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetCategoryPlurals();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{categoryID}")]
        public async Task<ActionResult> GetCategoryPlural_ByCategoryID(int categoryID) {

             var response = await _builder.GetCategoryPlural_ByCategoryID(categoryID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddCategoryPlural([FromBody]CategoryPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddCategoryPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetCategoryPlural_ByCategoryID", new {categoryID = ((CategoryPluralModel)response.Model).CategoryID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateCategoryPlural([FromBody]CategoryPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateCategoryPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetCategoryPlural_ByCategoryID", new {categoryID = model.CategoryID}, model);
        }
        
        [HttpDelete("{categoryID}")]
        public async Task<ActionResult> DeleteCategoryPlural(int categoryID) {

            var response = await _builder.DeleteCategoryPlural(categoryID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

