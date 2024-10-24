using Amazon.CognitoIdentityProvider.Model;
using SignUpRequest = CognitoAuth.Application.DTOs.SignUpRequest;

namespace CognitoAuth.Application.Services.Interfaces;

public interface ICognitoService
{
    public Task<SignUpResponse> SignUpAsync(SignUpRequest request);
    public Task<AdminInitiateAuthResponse> SignInAsync(string email, string password);
    public Task<ConfirmSignUpResponse> ConfirmSignUpAsync(string email, string confirmationCode);
    public Task GlobalSignOutAsync(string accessToken);
}