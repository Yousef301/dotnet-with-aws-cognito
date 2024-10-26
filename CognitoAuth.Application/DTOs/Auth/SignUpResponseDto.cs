namespace CognitoAuth.Application.DTOs.Auth;

public class SignUpResponseDto
{
    public string Email { get; set; }
    public bool UserConfirmed { get; set; }
}