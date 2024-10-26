using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using CognitoAuth.Application.DTOs.Auth;
using CognitoAuth.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CognitoAuth.Application.Services.Implementations;

public class CognitoService : ICognitoService
{
    private readonly IAmazonCognitoIdentityProvider _cognitoClient;
    private readonly string _userPoolId;
    private readonly string _clientId;

    public CognitoService(
        IAmazonCognitoIdentityProvider cognitoClient,
        IConfiguration configuration)
    {
        _cognitoClient = cognitoClient;
        _clientId = configuration["AWS:ClientId"]
                    ?? throw new ArgumentNullException(nameof(configuration));
        _userPoolId = configuration["UserPoolId"]
                      ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<SignUpResponse> SignUpAsync(SignUpRequestDto requestDto)
    {
        var signUpRequest = new SignUpRequest
        {
            ClientId = _clientId,
            Username = requestDto.Email,
            Password = requestDto.Password,
            UserAttributes = new List<AttributeType>
            {
                new AttributeType { Name = "name", Value = requestDto.Name }
            }
        };

        return await _cognitoClient.SignUpAsync(signUpRequest);
    }

    public async Task<AdminInitiateAuthResponse> SignInAsync(SignInRequestDto requestDto)
    {
        var authRequest = new AdminInitiateAuthRequest
        {
            UserPoolId = _userPoolId,
            ClientId = _clientId,
            AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
            AuthParameters = new Dictionary<string, string>
            {
                { "USERNAME", requestDto.Email },
                { "PASSWORD", requestDto.Password }
            }
        };

        return await _cognitoClient.AdminInitiateAuthAsync(authRequest);
    }

    public async Task<ConfirmSignUpResponse> ConfirmSignUpAsync(ConfirmEmailRequestDto requestDto)
    {
        var confirmSignUpRequest = new ConfirmSignUpRequest
        {
            ClientId = _clientId,
            Username = requestDto.Email,
            ConfirmationCode = requestDto.ConfirmationCode
        };

        return await _cognitoClient.ConfirmSignUpAsync(confirmSignUpRequest);
    }

    public async Task ResendConfirmationCodeAsync(string email)
    {
        var codeRequest = new ResendConfirmationCodeRequest
        {
            ClientId = _clientId,
            Username = email,
        };

        await _cognitoClient.ResendConfirmationCodeAsync(codeRequest);
    }

    public async Task GlobalSignOutAsync(string accessToken)
    {
        var signOutRequest = new GlobalSignOutRequest
        {
            AccessToken = accessToken
        };

        await _cognitoClient.GlobalSignOutAsync(signOutRequest);
    }

    public async Task<ForgotPasswordResponse> ForgotPasswordAsync(string email)
    {
        var request = new ForgotPasswordRequest
        {
            ClientId = _clientId,
            Username = email
        };

        return await _cognitoClient.ForgotPasswordAsync(request);
    }

    public async Task<ConfirmForgotPasswordResponse> ConfirmForgotPasswordAsync(ForgotPasswordRequestDto requestDto)
    {
        var forgotPasswordRequest = new ConfirmForgotPasswordRequest
        {
            ClientId = _clientId,
            Username = requestDto.Email,
            ConfirmationCode = requestDto.VerificationCode,
            Password = requestDto.NewPassword
        };

        return await _cognitoClient.ConfirmForgotPasswordAsync(forgotPasswordRequest);
    }

    public async Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequestDto request)
    {
        var changePasswordRequest = new ChangePasswordRequest
        {
            AccessToken = request.AccessToken,
            PreviousPassword = request.PreviousPassword,
            ProposedPassword = request.NewPassword
        };

        return await _cognitoClient.ChangePasswordAsync(changePasswordRequest);
    }
}