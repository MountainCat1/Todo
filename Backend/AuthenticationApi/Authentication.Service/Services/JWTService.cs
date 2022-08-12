using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Service.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Service.Services;

public interface IJWTService
{
    public string GenerateJwtToken(ClaimsIdentity claims);
}

public class JWTService : IJWTService
{
    private readonly JWTConfiguration _jwtConfiguration;

    public JWTService(JWTConfiguration jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration;
    }

    public string GenerateJwtToken(ClaimsIdentity claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfiguration.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddMinutes(_jwtConfiguration.Expires),
            Issuer = _jwtConfiguration.Issuer,
            Audience = _jwtConfiguration.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}