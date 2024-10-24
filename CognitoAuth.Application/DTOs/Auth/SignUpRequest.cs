namespace CognitoAuth.Application.DTOs.Auth;

public record SignUpRequest
{
    public string Email { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
}