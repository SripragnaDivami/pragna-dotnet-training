
using capstone_policy_management.DTOs.PolicyEnrollmentDTOs;
using capstone_policy_management.Filters;
using capstone_policy_management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace capstone_policy_management.Controllers
{
        [ApiController]
        [Route("api/policies")]
        [Authorize(Roles = "Admin,User")]
        [ServiceFilter(typeof(GlobalResponseFilter))]
         [ServiceFilter(typeof(PerformanceActionFilter))]
        public class PolicyController : ControllerBase
        {
            private readonly IPolicyService policyService;
           

            public PolicyController(IPolicyService _policyService)
            {
                this.policyService = _policyService;
            }
            
          
            [HttpGet (Name = "GetPolicies")]
            public async Task<IActionResult> GetPolicies()
            {
                // Return only active policies
                var policies = await policyService.GetPoliciesByStatusAsync(true);
                if (policies == null || !policies.Any())
                {
                    return NotFound(new { message = "No active policies found." });
                }
                return Ok(policies);
            }

            [HttpGet("{id}", Name = "GetPolicyById")]
            public async Task<IActionResult> GetPolicyById(int id)
            {
                var policy = await policyService.GetPolicyByIdAsync(id);
                if (policy == null)
                {
                    return NotFound(new { message = $"Policy with ID {id} not found." });
                }
                return Ok(policy);
            }

            [HttpGet("search", Name = "SearchPolicies")]
            public async Task<IActionResult> SearchPolicies([FromQuery] decimal minAmount, [FromQuery] decimal maxAmount)
            {
                var policies = await policyService.SearchPoliciesAsync(minAmount, maxAmount);
                return Ok(policies);
            }

            [HttpGet("status", Name = "GetPoliciesByStatus")]
            public async Task<IActionResult> GetPoliciesByStatus([FromQuery] bool isActive)
            {
                var policies = await policyService.GetPoliciesByStatusAsync(isActive);
                return Ok(policies);
            }
        }
}