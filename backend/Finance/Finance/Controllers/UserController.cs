using Finance.DTOs;
using Finance.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Controllers;
[ApiController]
[Route("api/[controller]")] // URL will be: api/user
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<UserResponse>> Register([FromBody] UserRegistrationRequest request)
    {
        var result = await _userService.RegisterAsync(request);
        if (result == null)
        {
            return BadRequest("Email already exists");
        }
        // TODO: Adicionar internacionalização via Localization
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetById(Guid id)
    {
        var result = await _userService.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponse>> Update(Guid id, [FromBody] UserUpdateRequest request)
    {
        var result = await _userService.UpdateByIdAsync(id,  request);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        var success = await _userService.DeleteByIdAsync(id);
        if (!success) 
        {
            return NotFound();
        }
        return NoContent();
    }
}