
using Shared.Core.Configurations.Common.Middleware;
using Shared.Persistence;
using BookStore.Persistence;
using WebApi.Extensions;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Swashbuckle.AspNetCore.SwaggerUI;
using Shared.Core.Configurations.Common.ExtensionMethods;
using Microsoft.OpenApi.Models;
using Shared.Core.Configurations.Security.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Core.Configurations.Security.Encryption;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
await builder.Services.AddSharedModule(builder.Configuration);
 builder.Services.AddPersistenceBookStoreApplicationServices(builder.Configuration);
builder.Services.AddCustomCarterModules(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.ConfigureApi();

builder.Services.ConfigureMySwagger();
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions!.Issuer,
            ValidAudience = tokenOptions!.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions!.SecurityKey)
        };
    });
builder.Services.AddAuthorization();
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Samet Türker Test Uygulamasý v1");
    c.DocExpansion(DocExpansion.None);
});
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<UserSessionMiddleware>();
app.MapCustomCarterModules();
app.Run();

