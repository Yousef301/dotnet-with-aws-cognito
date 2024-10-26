using Amazon.CognitoIdentityProvider.Model;
using CognitoAuth.Application.DTOs.Auth;

namespace CognitoAuth.Application.Services.Interfaces;

public interface ICognitoService
{
    public Task<SignUpResponse> SignUpAsync(SignUpRequestDto requestDto);
    public Task<AdminInitiateAuthResponse> SignInAsync(SignInRequestDto requestDto);
    public Task<ConfirmSignUpResponse> ConfirmSignUpAsync(ConfirmEmailRequestDto requestDto);
    public Task GlobalSignOutAsync(string accessToken);
    public Task ResendConfirmationCodeAsync(string email);
    public Task<ForgotPasswordResponse> ForgotPasswordAsync(string email);
    public Task<ConfirmForgotPasswordResponse> ConfirmForgotPasswordAsync(ForgotPasswordRequestDto requestDto);
}