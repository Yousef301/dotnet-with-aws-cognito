namespace CognitoAuth.Application.DTOs.Auth;

public record SignUpRequestDto : BaseAuthDto
{
    public string Name { get; init; }
    public string Password { get; init; }
}