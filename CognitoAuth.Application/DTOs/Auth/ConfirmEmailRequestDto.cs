namespace CognitoAuth.Application.DTOs.Auth;

public record ConfirmEmailRequestDto
{
    public string Email { get; set; }
    public string ConfirmationCode { get; set; }
}