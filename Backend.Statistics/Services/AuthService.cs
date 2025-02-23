using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Statistics.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Statistics.Services;

public class AuthService : IAuthService
{
    
    public string GenerateJwtToken(string username, string password)
    {
        var securityKey = new SymmetricSecurityKey("YourSecretKey"u8.ToArray());
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "yourdomain.com",
            audience: "yourdomain.com",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool CheckUserCredantionals(string? email, string? password)
    {
        throw new NotImplementedException();
    }
}