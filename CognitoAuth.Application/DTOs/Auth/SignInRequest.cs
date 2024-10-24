namespace CognitoAuth.Application.DTOs.Auth;

public record SignInRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
}