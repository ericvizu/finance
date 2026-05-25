using System.Security.Claims;
using Finance.DTOs.Salary;
using Finance.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SalaryController : ControllerBase
{
    private readonly ISalaryService _service;

    public SalaryController(ISalaryService service)
    {
        _service = service;
    }

    private Guid GetUserIdFromToken()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
        {
            throw new UnauthorizedAccessException("Invalid token or unidentified user.");
        }
        return userId;
    }

    [HttpPost]
    public async Task<ActionResult<SalaryResponse>> Create([FromBody] CreateSalaryRequest request)
    {
        try
        {
            var userId = GetUserIdFromToken();
            var result = await _service.CreateAsync(request, userId);
            
            return CreatedAtAction(nameof(GetById), new { id = result!.Id }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SalaryResponse>> GetById(Guid id)
    {
        var userId = GetUserIdFromToken();
        var result = await _service.GetByIdAsync(id, userId);
        
        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalaryResponse>>> GetAll()
    {
        var userId = GetUserIdFromToken();
        var result = await _service.GetAllByUserIdAsync(userId);
        
        return Ok(result);
    }

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<SalaryResponse>>> GetActive()
    {
        var userId = GetUserIdFromToken();
        var result = await _service.GetActiveByUserIdAsync(userId);
        
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SalaryResponse>> Update(Guid id, [FromBody] UpdateSalaryRequest request)
    {
        try
        {
            var userId = GetUserIdFromToken();
            var result = await _service.UpdateByIdAsync(id, request, userId);
            
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var userId = GetUserIdFromToken();
        var result = await _service.DeleteByIdAsync(id, userId);
        
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}