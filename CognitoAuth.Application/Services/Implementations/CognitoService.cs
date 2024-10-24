﻿using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
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

    public async Task<SignUpResponse> SignUpAsync(
        string email,
        string password)
    {
        var signUpRequest = new SignUpRequest
        {
            ClientId = _clientId,
            Username = email,
            Password = password,
            UserAttributes = new List<AttributeType>
            {
                new AttributeType { Name = "email", Value = email }
            }
        };

        return await _cognitoClient.SignUpAsync(signUpRequest);
    }

    public async Task<AdminInitiateAuthResponse> SignInAsync(
        string email,
        string password)
    {
        var authRequest = new AdminInitiateAuthRequest
        {
            UserPoolId = _userPoolId,
            ClientId = _clientId,
            AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
            AuthParameters = new Dictionary<string, string>
            {
                { "USERNAME", email },
                { "PASSWORD", password }
            }
        };

        return await _cognitoClient.AdminInitiateAuthAsync(authRequest);
    }

    public async Task<ConfirmSignUpResponse> ConfirmSignUpAsync(
        string email,
        string confirmationCode)
    {
        var confirmSignUpRequest = new ConfirmSignUpRequest
        {
            ClientId = _clientId,
            Username = email,
            ConfirmationCode = confirmationCode
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