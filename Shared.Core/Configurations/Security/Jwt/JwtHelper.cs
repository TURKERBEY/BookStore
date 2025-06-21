using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Core.Configurations.Common.ExtensionMethods;
using Shared.Core.Configurations.Security.Encryption;
using Shared.Persistence.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Configurations.Security.Jwt;
 public class JwtHelper : ITokenHelper
{
    public IConfiguration Configuration { get; }
    private TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;


    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

    }
    public AccessToken CreateToken(Kullanici user)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken
        {
            Token = token,
            Expiration = _accessTokenExpiration
        };

    }


    public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, Kullanici users,
        SigningCredentials signingCredentials)
    {
        var jwt = new JwtSecurityToken(

            issuer: tokenOptions.Issuer,
            audience: tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClaims(users),
            signingCredentials: signingCredentials
        );
        return jwt;
    }

    private IEnumerable<Claim> SetClaims(Kullanici user)
    {
        var claims = new List<Claim>();
        claims.AddNameIdentifier(user.Id.ToString());
 
        claims.AddUserName(user.UserName);
   

        return claims;
    }
}