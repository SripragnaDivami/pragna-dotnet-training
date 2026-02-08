using capstone_policy_management.DTOs.PolicyDTOs;
using capstone_policy_management.Filters;
using capstone_policy_management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace capstone_policy_management.Controllers
{
    [ApiController]
    [Route("api/admin/policies")]
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(GlobalResponseFilter))]
    [ServiceFilter(typeof(PerformanceActionFilter))]
    public class AdminPolicyController : ControllerBase
    {
        private readonly IPolicyService policyService;

        public AdminPolicyController(IPolicyService policyService)
        {
            this.policyService = policyService;
        }

        [HttpPost(Name = "AdminCreatePolicy")]
        public async Task<IActionResult> CreatePolicy([FromBody] PolicyCreateDto policyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var createdPolicy = await policyService.CreatePolicyAsync(policyDto);
            return CreatedAtRoute("GetPolicyById", new { id = createdPolicy.Id }, createdPolicy);
        }

        [HttpPut("{id}", Name = "AdminUpdatePolicy")]
        public async Task<IActionResult> UpdatePolicy(int id, [FromBody] PolicyUpdateDto policyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedPolicy = await policyService.UpdatePolicyAsync(id, policyDto);
            
            if (updatedPolicy == null)
            {
                return NotFound(new { message = "Policy not found." });
            }

            return Ok(updatedPolicy);
        }

        [HttpPatch("{id}/status", Name = "AdminUpdatePolicyStatus")]
        public async Task<IActionResult> UpdatePolicyStatus(int id, [FromQuery] bool isActive)
        {
            var updatedPolicy = await policyService.UpdatePolicyStatusAsync(id, isActive);
            
            if (updatedPolicy == null)
            {
                return NotFound(new { message = "Policy not found." });
            }

            return Ok(updatedPolicy);
        }
    }
}
