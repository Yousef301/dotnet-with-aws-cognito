using Amazon.CognitoIdentityProvider.Model;
using CognitoAuth.Application.DTOs.Auth;
using SignUpRequest = CognitoAuth.Application.DTOs.Auth.SignUpRequest;
using SignInRequest = CognitoAuth.Application.DTOs.Auth.SignInRequest;
using SignUpResponse = Amazon.CognitoIdentityProvider.Model.SignUpResponse;

namespace CognitoAuth.Application.Services.Interfaces;

public interface ICognitoService
{
    public Task<SignUpResponse> SignUpAsync(SignUpRequest request);
    public Task<AdminInitiateAuthResponse> SignInAsync(SignInRequest request);
    public Task<ConfirmSignUpResponse> ConfirmSignUpAsync(ConfirmEmailRequest request);
    public Task GlobalSignOutAsync(string accessToken);
}