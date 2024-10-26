namespace CognitoAuth.Application.DTOs.Auth;

public class ChangePasswordRequestDto
{
    public string? AccessToken { get; set; }
    public string PreviousPassword { get; set; }
    public string NewPassword { get; set; }
}