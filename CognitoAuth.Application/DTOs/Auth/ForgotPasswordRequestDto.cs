namespace CognitoAuth.Application.DTOs.Auth;

public record ForgotPasswordRequestDto : BaseAuthDto
{
    public string VerificationCode { get; set; }
    public string NewPassword { get; set; }
}