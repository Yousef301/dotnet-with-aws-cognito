using System.Net;
using CognitoAuth.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CognitoAuth;

public static class WebConfiguration
{
    public static IServiceCollection AddWebInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthenticationConfigurations(configuration)
            .AddApplicationInfrastructure();

        return services;
    }

    private static IServiceCollection AddAuthenticationConfigurations(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["Issuer"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Issuer"],
                    IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                    {
                        var cognitoJwksUrl = configuration["CognitoJwksUrl"];
                        var json = new WebClient().DownloadString(cognitoJwksUrl);
                        var keys = JsonWebKeySet.Create(json).Keys;
                        return keys;
                    }
                };
            });

        services.AddAuthorization();

        services.AddControllers();

        return services;
    }
}