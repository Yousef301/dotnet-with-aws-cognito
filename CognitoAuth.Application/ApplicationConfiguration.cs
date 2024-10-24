using Amazon.CognitoIdentityProvider;
using CognitoAuth.Application.Services.Implementations;
using CognitoAuth.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CognitoAuth.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAmazonCognitoIdentityProvider>();
        services.AddScoped<ICognitoService, CognitoService>();

        return services;
    }
}