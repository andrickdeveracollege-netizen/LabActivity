using LabActivity.Model.DTO;
using LabActivity.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabActivity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth) => _auth = auth;

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthRequestDto req)
    {
        var result = await _auth.RegisterAsync(req);
        if (result is null) return BadRequest("Username already taken.");
        return Created("", result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthRequestDto req)
    {
        var result = await _auth.LoginAsync(req);
        if (result is null) return Unauthorized("Invalid credentials.");
        return Ok(result);
    }
}