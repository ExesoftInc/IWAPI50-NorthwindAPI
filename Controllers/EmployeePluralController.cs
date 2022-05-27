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
    [Route("EmployeePlural")]
    public class EmployeePluralController : ControllerBase {
        
        private IEmployeePluralBuilder _builder;
        
        public EmployeePluralController(IEmployeePluralBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetEmployeePlurals() {

            return Ok(await _builder.GetEmployeePlurals());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(EmployeePluralModel.EmployeeID));
            propNames.Add(nameof(EmployeePluralModel.LastName));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetEmployeePlurals();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{employeeID}")]
        public async Task<ActionResult> GetEmployeePlural_ByEmployeeID(int employeeID) {

             var response = await _builder.GetEmployeePlural_ByEmployeeID(employeeID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetEmployeePlural_ByReportsTo/{reportsTo}")]
        public async Task<IQueryable<EmployeePluralModel>> GetEmployeePlural_ByReportsTo(int reportsTo) {

            return await _builder.GetEmployeePlural_ByReportsTo(reportsTo);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddEmployeePlural([FromBody]EmployeePluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddEmployeePlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetEmployeePlural_ByEmployeeID", new {employeeID = ((EmployeePluralModel)response.Model).EmployeeID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateEmployeePlural([FromBody]EmployeePluralModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateEmployeePlural(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetEmployeePlural_ByEmployeeID", new {employeeID = model.EmployeeID}, model);
        }
        
        [HttpDelete("{employeeID}")]
        public async Task<ActionResult> DeleteEmployeePlural(int employeeID) {

            var response = await _builder.DeleteEmployeePlural(employeeID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

