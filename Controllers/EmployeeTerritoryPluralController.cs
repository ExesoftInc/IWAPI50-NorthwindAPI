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
    [Route("EmployeeTerritoryPlural")]
    public class EmployeeTerritoryPluralController : ControllerBase {
        
        private IEmployeeTerritoryPluralBuilder _builder;
        
        public EmployeeTerritoryPluralController(IEmployeeTerritoryPluralBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetEmployeeTerritoryPlurals() {

            return Ok(await _builder.GetEmployeeTerritoryPlurals());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(EmployeeTerritoryPluralModel.EmployeeID));
            propNames.Add(nameof(EmployeeTerritoryPluralModel.TerritoryID));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetEmployeeTerritoryPlurals();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{employeeID}/{territoryID}")]
        public async Task<ActionResult> GetEmployeeTerritoryPlural_ByEmployeeIDTerritoryID(int employeeID, string territoryID) {

             var response = await _builder.GetEmployeeTerritoryPlural_ByEmployeeIDTerritoryID(employeeID, territoryID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetEmployeeTerritoryPlural_ByEmployeeID/{employeeID}")]
        public async Task<IQueryable<EmployeeTerritoryPluralModel>> GetEmployeeTerritoryPlural_ByEmployeeID(int employeeID) {

            return await _builder.GetEmployeeTerritoryPlural_ByEmployeeID(employeeID);
        }
        
        [HttpGet("GetEmployeeTerritoryPlural_ByTerritoryID/{territoryID}")]
        public async Task<IQueryable<EmployeeTerritoryPluralModel>> GetEmployeeTerritoryPlural_ByTerritoryID(string territoryID) {

            return await _builder.GetEmployeeTerritoryPlural_ByTerritoryID(territoryID);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddEmployeeTerritoryPlural([FromBody]EmployeeTerritoryPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddEmployeeTerritoryPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetEmployeeTerritoryPlural_ByEmployeeIDTerritoryID", new {employeeID = ((EmployeeTerritoryPluralModel)response.Model).EmployeeID, territoryID = ((EmployeeTerritoryPluralModel)response.Model).TerritoryID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateEmployeeTerritoryPlural([FromBody]EmployeeTerritoryPluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateEmployeeTerritoryPlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetEmployeeTerritoryPlural_ByEmployeeIDTerritoryID", new {employeeID = model.EmployeeID, territoryID = model.TerritoryID}, model);
        }
        
        [HttpDelete("{employeeID}/{territoryID}")]
        public async Task<ActionResult> DeleteEmployeeTerritoryPlural(int employeeID, string territoryID) {

            var response = await _builder.DeleteEmployeeTerritoryPlural(employeeID, territoryID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

