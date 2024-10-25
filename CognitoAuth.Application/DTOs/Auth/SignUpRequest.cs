﻿namespace CognitoAuth.Application.DTOs.Auth;

public record SignUpRequest
{
    public string Email { get; init; }
    public string Name { get; init; }
    public string Password { get; init; }
}