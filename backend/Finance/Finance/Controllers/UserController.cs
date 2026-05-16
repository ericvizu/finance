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
        return CreatedAtAction(nameof(Register), new { id = result.Id }, result);
    }
}