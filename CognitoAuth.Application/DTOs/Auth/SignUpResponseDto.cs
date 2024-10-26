namespace CognitoAuth.Application.DTOs.Auth;

public record SignUpResponseDto : BaseAuthDto
{
    public bool UserConfirmed { get; set; }
}