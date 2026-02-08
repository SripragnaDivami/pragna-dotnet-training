using capstone_policy_management.DTOs.PolicyEnrollmentDTOs;
using capstone_policy_management.Filters;
using capstone_policy_management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace capstone_policy_management.Controllers
{
    [ApiController]
    [Route("api/admin/enrollments")]
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(GlobalResponseFilter))]
    public class AdminEnrollmentController : ControllerBase
    {
        private readonly IPolicyEnrollmentService policyEnrollmentService;

        public AdminEnrollmentController(IPolicyEnrollmentService policyEnrollmentService)
        {
            this.policyEnrollmentService = policyEnrollmentService;
        }

        [HttpGet(Name = "AdminGetEnrollmentsByStatus")]
        public async Task<IActionResult> GetEnrollmentsByStatus([FromQuery] string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                var allEnrollments = await policyEnrollmentService.GetAllEnrollmentsAsync();
                return Ok(allEnrollments);
            }

            var enrollments = await policyEnrollmentService.GetEnrollmentsByStatusAsync(status);
            return Ok(enrollments);
        }

        [HttpPatch("{id}/approve", Name = "AdminApproveEnrollment")]
        public async Task<IActionResult> ApproveEnrollment(int id)
        {
            var enrollment = await policyEnrollmentService.ApproveEnrollmentAsync(id);
            if (enrollment == null)
            {
                return NotFound(new { message = $"Policy enrollment with ID {id} not found." });
            }
            return Ok(enrollment);
        }

        [HttpPatch("{id}/reject", Name = "AdminRejectEnrollment")]
        public async Task<IActionResult> RejectEnrollment(int id)
        {
            var enrollment = await policyEnrollmentService.RejectEnrollmentAsync(id);
            if (enrollment == null)
            {
                return NotFound(new { message = $"Policy enrollment with ID {id} not found." });
            }
            return Ok(enrollment);
        }
    }
}
