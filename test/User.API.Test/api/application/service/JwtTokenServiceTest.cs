using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using User.API.application.service;
using User.API.exceptions;
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
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../../../User.API.Test"))
            .AddJsonFile("appsettings.Test.json", optional: true, reloadOnChange: true)
            .Build();
        this._jwtTokenService = new JwtTokenService(_configuration);
    }

    [Fact]
    public void ValidateJwtToken_ReturnTrue()
    {
        //arrange
        var token = new JwtSecurityTokenHandler().WriteToken(GenerateJwtToken(Email));
        token = $"Bearer {token}";

        //act
        var result = _jwtTokenService.ValidateJwtToken(token);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateJwtToken_ReturnFalse()
    {
        //arrange
        var token = new JwtSecurityTokenHandler().WriteToken(GenerateJwtToken(Email));
        token = $"Bearer {token} test";

        //act
        var result = _jwtTokenService.ValidateJwtToken(token);

        //assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateJwtToken_ExpiredTokenReturnsFalse()
    {
        //arrange
        //期限切れのトークンを作成
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds().ToString())
        };
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Jwt_Key") ?? string.Empty));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var expiredToken = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("Jwt:Issuer"),
            audience: _configuration.GetValue<string>("Jwt:Audience"),
            claims: claims,
            expires: DateTime.Now,
            signingCredentials: credentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(expiredToken);
        token = $"Bearer {token}";

        //act
        var result = _jwtTokenService.ValidateJwtToken(token);

        //assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateJwtToken_TokenWithInvalidIssuerReturnsFalse()
    {
        //arrange
        //期限切れのトークンを作成
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds().ToString())
        };
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Jwt_Key") ?? string.Empty));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var expiredToken = new JwtSecurityToken(
            issuer: "test",
            audience: _configuration.GetValue<string>("Jwt:Audience"),
            claims: claims,
            expires: DateTime.Now,
            signingCredentials: credentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(expiredToken);
        token = $"Bearer {token}";

        //act
        var result = _jwtTokenService.ValidateJwtToken(token);

        //assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateJwtToken_TokenWithInvalidAudienceReturnsFalse()
    {
        //arrange
        //期限切れのトークンを作成
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds().ToString())
        };
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Jwt_Key") ?? string.Empty));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var expiredToken = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("Jwt:Issuer"),
            audience: "test",
            claims: claims,
            expires: DateTime.Now,
            signingCredentials: credentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(expiredToken);
        token = $"Bearer {token}";

        //act
        var result = _jwtTokenService.ValidateJwtToken(token);

        //assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateJwtToken_TokenWithoutBearerPrefixIsValid()
    {
        //arrange
        var token = new JwtSecurityTokenHandler().WriteToken(GenerateJwtToken(Email));

        //act & assert
        Assert.Throws<JwtTokenException>(() => _jwtTokenService.ValidateJwtToken(token));
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