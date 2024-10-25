using Amazon.CognitoIdentityProvider;
using CognitoAuth.Application.Services.Implementations;
using CognitoAuth.Application.Services.Interfaces;
using CognitoAuth.Application.Validators.Auth;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace CognitoAuth.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationInfrastructure(this IServiceCollection services)
    {
        services.AddAWSService<IAmazonCognitoIdentityProvider>()
            .AddScoped<ICognitoService, CognitoService>()
            .AddFluentValidationAutoValidation();

        return services;
    }

    private static IServiceCollection AddFluentValidationConfigurations(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblyContaining<SignUpRequestValidator>();

        return services;
    }
}