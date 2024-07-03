using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using User.API.exceptions;

namespace User.API.application.service;
public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public bool ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        if (!token.Contains("Bearer"))
        {
            throw new JwtTokenException("Bearer識別子が無効です。");
        }

        var Token = token.Replace("Bearer", "").Trim();

        try
        {
            tokenHandler.ValidateToken(Token, new TokenValidationParameters
            {
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Jwt_Key") ?? string.Empty)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }
}