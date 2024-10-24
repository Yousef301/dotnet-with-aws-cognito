using CognitoAuth.Application.DTOs.Auth;
using CognitoAuth.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CognitoAuth.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ICognitoService _cognitoService;

    public AuthController(ICognitoService cognitoService)
    {
        _cognitoService = cognitoService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        try
        {
            var result = await _cognitoService.SignUpAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        try
        {
            var result = await _cognitoService.SignInAsync(request);
            return Ok(new { token = result.AuthenticationResult.AccessToken });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmSignUp([FromBody] ConfirmEmailRequest request)
    {
        try
        {
            var result = await _cognitoService.ConfirmSignUpAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}