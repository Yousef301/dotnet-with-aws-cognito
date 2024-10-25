namespace CognitoAuth.Application.DTOs.Auth;

public class SignUpResponse
{
    public string Email { get; set; }
    public bool UserConfirmed { get; set; }
}