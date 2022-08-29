using System.Security.Cryptography;
using Authentication.Service.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Api.Extensions;

public static class ServiceCollectionExtensions
{ 
    public static IServiceCollection AddAsymmetricAuthentication(this IServiceCollection services, JWTConfiguration jwtConfig)
    {
        services.AddSingleton<RsaSecurityKey>(provider => {
            // It's required to register the RSA key with depedency injection.
            // If you don't do this, the RSA instance will be prematurely disposed.
                
            RSA rsa = RSA.Create();
            rsa.ImportRSAPublicKey(
                source: Convert.FromBase64String(jwtConfig.PublicKey),
                bytesRead: out int _
            );
                
            return new RsaSecurityKey(rsa);
        });
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                SecurityKey key = services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();
                    
                options.IncludeErrorDetails = true; // <- great for debugging

                // Configure the actual Bearer validation
                options.TokenValidationParameters = new TokenValidationParameters {
                    IssuerSigningKey = key,
                    ValidAudience = jwtConfig.Audience,
                    ValidIssuer = jwtConfig.Issuer,
                    RequireSignedTokens = true,
                    RequireExpirationTime = true, // <- JWTs are required to have "exp" property set
                    ValidateLifetime = true, // <- the "exp" will be validated
                    ValidateAudience = true,
                    ValidateIssuer = true,
                };
            });

        return services;
    }
}