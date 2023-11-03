using System.Security.Claims;
using Entities.DTOs.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace SlydynBackend.Controllers;

[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
  private readonly IServiceManager _service;

  public AuthenticationController(IServiceManager service)
  {
    _service = service;
  }

  [HttpPost("register")]
  public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto userDto)
  {
    var result = await _service.AuthService.RegisterUser(userDto);
    if (result.Succeeded) return StatusCode(201);
    foreach (var error in result.Errors)
    {
      ModelState.TryAddModelError(error.Code, error.Description);
    }

    return BadRequest(ModelState);
  }

  [HttpPost("login")]
  public async Task<IActionResult> LoginUser([FromBody] LoginUserDto userDto)
  {
    var result = await _service.AuthService.LoginUser(userDto);
    Response.Cookies.Append("refreshToken", result.RefreshToken!, new CookieOptions()
    {
      HttpOnly = true,
      Secure = false
    });
    return Ok(result);
  }

  [HttpGet("{username}")]
  [Authorize]
  public async Task<ActionResult<UserDto>> GetUser(string username)
  {
    var user = await _service.AuthService.GetUserPublicInfo(username);

    return StatusCode(StatusCodes.Status200OK, user);
  }

  [HttpGet("me")]
  [Authorize]
  public async Task<ActionResult<UserDto>> GetMe()
  {
    // get username from token
    var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    if (username == null)
    {
      throw new BadHttpRequestException("username is not defined in JWT token");
    }

    var user = await _service.AuthService.GetUserPublicInfo(username);

    return StatusCode(StatusCodes.Status200OK, user);
  }

  [HttpGet("test-auth")]
  [Authorize]
  public IActionResult TestAuth()
  {
    return Ok(null);
  }

  [HttpGet("test-auth-superadmin")]
  [Authorize(Roles = "SuperAdmin")]
  public IActionResult TestAuthSuperAdmin()
  {
    return Ok(null);
  }

  [HttpGet("test-auth-consumer")]
  [Authorize(Roles = "Consumer")]
  public IActionResult TestAuthConsumer()
  {
    return Ok(null);
  }
}