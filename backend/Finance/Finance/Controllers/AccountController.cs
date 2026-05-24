using System.Security.Claims;
using Finance.DTOs.Account;
using Finance.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
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
    public async Task<ActionResult<AccountResponse>> Register([FromBody] CreateAccountRequest request)
    {
        var userId = GetUserIdFromToken();
        var result = await _accountService.CreateAsync(request,  userId);
        if (result == null)
        {
            return Conflict("Account with this name already exists.");
        }
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountResponse>> GetById(Guid id)
    {
        var userId = GetUserIdFromToken();
        var result = await _accountService.GetByIdAsync(id, userId);
        if (result == null)
        {
            return  NotFound();
        }
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAll()
    {
        var userId = GetUserIdFromToken();
        var result = await _accountService.GetAllByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AccountResponse>> Update(Guid id, [FromBody] UpdateAccountRequest request)
    {
        var userId = GetUserIdFromToken();
        var result = await _accountService.UpdateByIdAsync(id, request,  userId);
        if (result == null)
        {
            return  NotFound();
        }
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var userId = GetUserIdFromToken();
        var result = await _accountService.DeleteByIdAsync(id, userId);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
    
}