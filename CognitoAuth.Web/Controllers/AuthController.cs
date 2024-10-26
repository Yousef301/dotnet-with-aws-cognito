using CognitoAuth.Application.DTOs.Auth;
using CognitoAuth.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto requestDto)
    {
        try
        {
            var result = await _cognitoService.SignUpAsync(requestDto);

            return Ok(new SignUpResponseDto
            {
                Email = requestDto.Email,
                UserConfirmed = result.UserConfirmed
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequestDto requestDto)
    {
        try
        {
            var result = await _cognitoService.SignInAsync(requestDto);
            return Ok(new { token = result.AuthenticationResult.AccessToken });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmSignUp([FromBody] ConfirmEmailRequestDto requestDto)
    {
        try
        {
            var result = await _cognitoService.ConfirmSignUpAsync(requestDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader!.Substring("Bearer ".Length).Trim();

            await _cognitoService.GlobalSignOutAsync(token);
            return Ok(new { message = "Successfully signed out" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}