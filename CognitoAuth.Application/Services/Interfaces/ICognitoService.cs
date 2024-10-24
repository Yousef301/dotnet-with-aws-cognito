using Amazon.CognitoIdentityProvider.Model;

namespace CognitoAuth.Application.Services.Interfaces;

public interface ICognitoService
{
    public Task<SignUpResponse> SignUpAsync(string email, string password);
    public Task<AdminInitiateAuthResponse> SignInAsync(string email, string password);
    public Task<ConfirmSignUpResponse> ConfirmSignUpAsync(string email, string confirmationCode);
    public Task GlobalSignOutAsync(string accessToken);
}