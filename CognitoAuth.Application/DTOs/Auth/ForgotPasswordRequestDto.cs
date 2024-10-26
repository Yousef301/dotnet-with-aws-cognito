namespace CognitoAuth.Application.DTOs.Auth;

public class ForgotPasswordRequestDto
{
    public string Email { get; set; }
    public string VerificationCode { get; set; }
    public string NewPassword { get; set; }
}