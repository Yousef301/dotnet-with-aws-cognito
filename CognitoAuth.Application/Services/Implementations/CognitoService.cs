using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using CognitoAuth.Application.DTOs.Auth;
using CognitoAuth.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using SignUpRequest = Amazon.CognitoIdentityProvider.Model.SignUpRequest;
using SignUpResponse = Amazon.CognitoIdentityProvider.Model.SignUpResponse;

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

    public async Task<SignUpResponse> SignUpAsync(DTOs.Auth.SignUpRequest request)
    {
        var signUpRequest = new SignUpRequest
        {
            ClientId = _clientId,
            Username = request.Email,
            Password = request.Password,
            UserAttributes = new List<AttributeType>
            {
                new AttributeType { Name = "name", Value = request.Name }
            }
        };

        return await _cognitoClient.SignUpAsync(signUpRequest);
    }

    public async Task<AdminInitiateAuthResponse> SignInAsync(SignInRequest request)
    {
        var authRequest = new AdminInitiateAuthRequest
        {
            UserPoolId = _userPoolId,
            ClientId = _clientId,
            AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
            AuthParameters = new Dictionary<string, string>
            {
                { "USERNAME", request.Email },
                { "PASSWORD", request.Password }
            }
        };

        return await _cognitoClient.AdminInitiateAuthAsync(authRequest);
    }

    public async Task<ConfirmSignUpResponse> ConfirmSignUpAsync(ConfirmEmailRequest request)
    {
        var confirmSignUpRequest = new ConfirmSignUpRequest
        {
            ClientId = _clientId,
            Username = request.Email,
            ConfirmationCode = request.ConfirmationCode
        };

        return await _cognitoClient.ConfirmSignUpAsync(confirmSignUpRequest);
    }

    public async Task GlobalSignOutAsync(string accessToken)
    {
        var signOutRequest = new GlobalSignOutRequest
        {
            AccessToken = accessToken
        };

        await _cognitoClient.GlobalSignOutAsync(signOutRequest);
    }
}