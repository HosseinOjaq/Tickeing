using System.Net;
using System.Text;
using Ticketing.Common.Enums;
using System.Security.Claims;
using Ticketing.Common.Exceptions;
using Microsoft.IdentityModel.Tokens;
using Ticketing.Domain.Models.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Ticketing.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
    {
        var secretKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
        var encryptionKey = Encoding.UTF8.GetBytes(jwtSettings.EncryptKey);
        var tokenValidationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            RequireSignedTokens = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
        };

        services.AddScoped(x => tokenValidationParameters);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = tokenValidationParameters;
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception != null)
                        throw new AppException
                        (OperationStatusCode.UnAuthorized,
                        "Authentication failed.",
                        HttpStatusCode.Unauthorized,
                        context.Exception, null);

                    return Task.CompletedTask;
                },
                OnTokenValidated = async context =>
                {
                    var claimsIdentity = context.Principal!.Identity as ClaimsIdentity;
                    if (claimsIdentity!.Claims?.Any() != true)
                        context.Fail("توکن نامعتبر می باشد");                    
                },
                OnChallenge = context =>
                {
                    if (context.AuthenticateFailure != null)
                        throw new AppException
                        (OperationStatusCode.UnAuthorized,
                        "Authenticate failure.",
                        HttpStatusCode.Unauthorized,
                        context.AuthenticateFailure, null);

                    throw new AppException
                    (OperationStatusCode.UnAuthorized,
                    "You are unauthorized to access this resource.",
                    HttpStatusCode.Unauthorized);
                }
            };
        });
    }
    public static void AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy("CustomCors", builder =>
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
        ));
    }
}