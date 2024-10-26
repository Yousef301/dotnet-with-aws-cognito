namespace CognitoAuth.Application.DTOs.Auth;

public record SignInRequestDto : BaseAuthDto
{
    public string Password { get; init; }
}