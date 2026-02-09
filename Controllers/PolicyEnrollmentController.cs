using capstone_policy_management.DTOs.PolicyEnrollmentDTOs;
using capstone_policy_management.Filters;
using capstone_policy_management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace capstone_policy_management.Controllers;

[ApiController]
[ServiceFilter(typeof(GlobalResponseFilter))]
[Route("api/policy-enrollments")]
[Authorize(Roles = "Admin")]
public class PolicyEnrollmentController : ControllerBase
{
    private readonly IPolicyEnrollmentService policyEnrollmentService;

    public PolicyEnrollmentController(IPolicyEnrollmentService policyEnrollmentService)
    {
        this.policyEnrollmentService = policyEnrollmentService;
    }

    [HttpGet(Name = "GetAllEnrollments")]
    public async Task<IActionResult> GetAllEnrollments()
    {
        var enrollments = await policyEnrollmentService.GetAllEnrollmentsAsync();
        return Ok(enrollments);
    }


    [HttpGet("status/{status}", Name = "GetEnrollmentsByStatus")]
    public async Task<IActionResult> GetEnrollmentsByStatus(string status)
    {
        var enrollments = await policyEnrollmentService.GetEnrollmentsByStatusAsync(status);
        return Ok(enrollments);
    }

    [HttpGet("{id}", Name = "GetEnrollmentById")]
    public async Task<IActionResult> GetEnrollmentById(int id)
    {
        var enrollment = await policyEnrollmentService.GetEnrollmentByIdAsync(id);
        if (enrollment == null)
        {
            return NotFound(new { message = $"Policy enrollment with ID {id} not found." });
        }
        return Ok(enrollment);
    }

    [HttpPost(Name = "CreateEnrollment")]
    public async Task<IActionResult> CreateEnrollment([FromBody] PolicyEnrollmentCreateDto enrollmentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var enrollment = await policyEnrollmentService.CreateEnrollmentAsync(enrollmentDto);
        return CreatedAtRoute("GetEnrollmentById", new { id = enrollment.Id }, enrollment);
    }

    [HttpPut("{id}", Name = "UpdateEnrollment")]
    public async Task<IActionResult> UpdateEnrollment(int id, [FromBody] PolicyEnrollmentUpdateDto enrollmentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var enrollment = await policyEnrollmentService.UpdateEnrollmentAsync(id, enrollmentDto);
        if (enrollment == null)
        {
            return NotFound(new { message = $"Policy enrollment with ID {id} not found." });
        }
        return Ok(enrollment);
    }

    [HttpDelete("{id}", Name = "DeleteEnrollment")]
    public async Task<IActionResult> DeleteEnrollment(int id)
    {
        var existing = await policyEnrollmentService.GetEnrollmentByIdAsync(id);
        if (existing == null)
        {
            return NotFound(new { message = $"Policy enrollment with ID {id} not found." });
        }

        await policyEnrollmentService.DeleteEnrollmentAsync(id);
        return Ok(new { message = "Policy enrollment deleted successfully." });
    }
}