namespace CognitoAuth.Application.DTOs.Auth;

public record ConfirmEmailRequestDto : BaseAuthDto
{
    public string ConfirmationCode { get; set; }
}