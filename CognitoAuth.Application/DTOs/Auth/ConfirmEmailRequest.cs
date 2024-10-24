namespace CognitoAuth.Application.DTOs.Auth;

public record ConfirmEmailRequest
{
    public string Email { get; set; }
    public string ConfirmationCode { get; set; }
}