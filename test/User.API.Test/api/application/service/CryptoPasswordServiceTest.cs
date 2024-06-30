using Microsoft.Extensions.Configuration;
using User.API.application.service;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace User.API.Test.application.service;

public class CryptoPasswordServiceTest
{
    private readonly IConfiguration _configuration;
    private readonly string password = "satoshi";
    private readonly CryptoPasswordService _cryptoPasswordService;

    public CryptoPasswordServiceTest()
    {
        this._configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .Build();
        this._cryptoPasswordService = new CryptoPasswordService(_configuration);
    }

    [Fact]
    public void CreateSalt_ReturnsCorrectLength()
    {
        // arrange
        // var IterationCount = _configuration.GetValue<int>("PasswordHashSettings:IterationCount");
        // var NumBytesRequested = _configuration.GetValue<int>("PasswordHashSettings:NumBytesRequested");

        // act
        var salt = _cryptoPasswordService.CreateSalt();

        // assert
        Assert.Equal(24, salt.Length);
    }

    [Fact]
    public void CreateSalt_ReturnsUniqueSalts()
    {
        //act
        var salt1 = _cryptoPasswordService.CreateSalt();
        var salt2 = _cryptoPasswordService.CreateSalt();

        //assert
        Assert.NotEqual(salt1, salt2);
    }

    [Fact]
    public void CreateSalt_ReturnsValidBase64Encoding()
    {
        //act
        var salt = _cryptoPasswordService.CreateSalt();

        //assert
        Assert.True(IsBase64Encoding(salt));
    }

    [Fact]
    public async Task HashPassword_ReturnsValidPasswordNotNullAndNotEmpty()
    {
        //act
        var hashPassword = await _cryptoPasswordService.HashPassword(this.password);

        //assert
        Assert.NotNull(hashPassword.Password);
        Assert.NotNull(hashPassword.Salt);
        Assert.NotEmpty(hashPassword.Password);
        Assert.NotEmpty(hashPassword.Salt);
    }

    [Fact]
    public async Task HashPassword_ReturnsUniquePassword()
    {
        //act
        var hashPassword1 = await _cryptoPasswordService.HashPassword(this.password);
        var hashPassword2 = await _cryptoPasswordService.HashPassword(this.password);

        //assert
        Assert.NotEqual(hashPassword1.Password, hashPassword2.Password);
        Assert.NotEqual(hashPassword1.Salt, hashPassword2.Salt);
    }

    [Fact]
    public async Task HashPassword_ReturnsValidBase64Encoding()
    {
        //act
        var hashPassword = await _cryptoPasswordService.HashPassword(this.password);

        //assert
        Assert.True(IsBase64Encoding(hashPassword.Password));
        Assert.True(IsBase64Encoding(hashPassword.Salt));
    }

    [Theory]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("あいうえお")]
    [InlineData("!@#$%^&*()_+")]
    [InlineData("VeryLongPasswordVeryLongPasswordVeryLongPasswordVeryLongPassword")]
    public async Task HashPassword_HandlesVariousInputs(string password)
    {
        //act
        var hashPassword = await _cryptoPasswordService.HashPassword(password);
        //assert
        Assert.NotNull(hashPassword.Password);
        Assert.NotNull(hashPassword.Salt);
        Assert.NotEmpty(hashPassword.Password);
        Assert.NotEmpty(hashPassword.Salt);
    }

    private bool IsBase64Encoding(string base64)
    {
        Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
        return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
    }
}