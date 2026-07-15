using HappyChat.Application.Contracts.Services;
using HappyChat.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HappyChat.Infrastructure.Services;

public class JWTService : IJWTService
{
    private readonly IConfiguration _configuration;
    public JWTService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(User User)
    {
        var userRoles = User.UserRoles;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
            new Claim(ClaimTypes.Name, User.Username),
            new Claim(ClaimTypes.Role, User.UserRoles.FirstOrDefault(x => x.Role.Title.ToLower() == "user").Role.Title)
        };


        if (userRoles.Any(x => x.Role.Title.ToLower() == "sysadmin"))
        {
            claims.Add(new Claim(ClaimTypes.Role, User.UserRoles.FirstOrDefault(x => x.Role.Title.ToLower() == "sysadmin").Role.Title));
        }

        if (userRoles.Any(x => x.Role.Title.ToLower() == "admin"))
        {
            claims.Add(new Claim(ClaimTypes.Role, User.UserRoles.FirstOrDefault(x => x.Role.Title.ToLower() == "admin").Role.Title));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[32];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomBytes);

        return Convert.ToBase64String(randomBytes);
    }
}