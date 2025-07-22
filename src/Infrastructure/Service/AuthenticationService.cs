using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DHAFacilitationAPIs.Application.Common.Models;
using DHAFacilitationAPIs.Application.Common.Settings;
using DHAFacilitationAPIs.Application.Interface.Service;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DHAFacilitationAPIs.Infrastructure.Service;

public class AuthenticationService : IAuthenticationService
{
    private readonly JwtSettings _jwtSettings;

    public AuthenticationService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    /// <summary>
    /// Generate a JWT with the given CNIC and role.
    /// </summary>
    public Task<string> GenerateTokenAsync(string cnic, string? role = "User")
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, cnic),
            new Claim(ClaimTypes.Role, role ?? "User")
        };

        var token = GenerateAccessToken(claims);
        return Task.FromResult(token);
    }

    /// <summary>
    /// Generate a JWT with a custom list of claims.
    /// </summary>
    public Task<string> GenerateTokenAsync(IEnumerable<Claim> claims)
    {
        var token = GenerateAccessToken(claims);
        return Task.FromResult(token);
    }

    #region Private Method

    private string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signinCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return tokenString;
    }

    #endregion
}
