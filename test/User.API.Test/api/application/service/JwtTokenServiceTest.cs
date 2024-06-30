using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using User.API.application.service;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace User.API.Test.application.service;

public class JwtTokenServiceTest
{
    private readonly IConfiguration _configuration;
    private readonly JwtTokenService _jwtTokenService;
    private readonly string Email = "satoshi@sample.com";

    public JwtTokenServiceTest()
    {
        this._configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .Build();
        this._jwtTokenService = new JwtTokenService(_configuration);
    }

    [Fact]
    public void ValidateJwtToken_ReturnTrue()
    {
        //arrange
        var token = new JwtSecurityTokenHandler().WriteToken(GenerateJwtToken(Email));

        //act
        var result = _jwtTokenService.ValidateJwtToken(token);

        //assert
        Assert.True(result);
    }

    private JwtSecurityToken GenerateJwtToken(string email)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds().ToString())
        };
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Jwt_Key") ?? string.Empty));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("Jwt:Issuer"),
            audience: _configuration.GetValue<string>("Jwt:Audience"),
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );

        return token;
    }
}