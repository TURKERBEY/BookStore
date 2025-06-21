using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Core.Configurations.Security.Encryption;
using Shared.Core.Configurations.Security.Jwt;
using System.Reflection;


namespace WebApi.Extensions;
public static class ServiceExtensions
{
    public static void ConfigureApi(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<ITokenHelper, JwtHelper>();
        //services.AddScoped<IOperationFilter, AuthResponsesOperationFilter>();
        services.AddHttpContextAccessor();

    }
    public static void ConfigureMySwagger(this IServiceCollection services)
    {
         

        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Samet Türker Test Uygulaması", Version = "v1" });
            opt.DocInclusionPredicate((name, api) => true);
            opt.AddSecurityDefinition(
       name: "Bearer",
       securityScheme: new OpenApiSecurityScheme
       {
           Name = "Authorization",
           Type = SecuritySchemeType.Http,
           //Type = SecuritySchemeType.ApiKey,
           Scheme = "Bearer",
           BearerFormat = "JWT",
           In = ParameterLocation.Header,
           Description =
               "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345.54321\""
       }
   );
            opt.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                new string[] { }
            }
                }
            );
        });
    }


    public static void ConfigureAuthentication(this IServiceCollection services, TokenOptions tokenOptions)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                };
            });
    }

}
