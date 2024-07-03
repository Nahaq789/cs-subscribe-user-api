using Microsoft.AspNetCore.Mvc;
using User.domain.model;
using User.Domain.exceptions;

namespace User.API.Test.domain;

public class UserEntityTest
{
    private readonly Guid _aggregateId;

    public UserEntityTest()
    {
        this._aggregateId = Guid.NewGuid();
    }

    [Theory]
    [InlineData("userexample.com")]  // @ が欠けている
    [InlineData("user@")]  // ドメインが欠けている
    [InlineData("@example.com")]  // ローカル部分が欠けている
    [InlineData("user@example")]  // トップレベルドメインが欠けている
    [InlineData("user@.com")]  // ドメイン名が欠けている
    [InlineData("user@example..com")]  // 連続したドット
    [InlineData("user@-example.com")]  // ドメインがハイフンで始まっている
    [InlineData("user@example-.com")]  // ドメインがハイフンで終わっている
    [InlineData("user@exam_ple.com")]  // ドメインにアンダースコアを含む
    [InlineData("user name@example.com")]  // スペースを含む
    [InlineData("user@exam ple.com")]  // ドメインにスペースを含む
    [InlineData("user@example.c")]  // トップレベルドメインが1文字
    [InlineData("user@@example.com")]  // 連続した@
    [InlineData(".user@example.com")]  // ローカル部分がドットで始まっている
    [InlineData("user.@example.com")]  // ローカル部分がドットで終わっている
    [InlineData("us..er@example.com")]  // ローカル部分に連続したドット
    [InlineData("")]  // 空文字列
    [InlineData("あいうえお@example.com")]  // 日本語文字を含む
    [InlineData("user@あいうえお.com")]  // ドメインに日本語文字を含む
    [InlineData("user@example.com@example.com")]  // 複数の@
    [InlineData("user:name@example.com")]  // 許可されていない特殊文字を含む
    public void IsValidEmail_InvalidEmailThrowsException(string email)
    {
        //act & assert
        Assert.Throws<UserDomainException>(() => new UserEntity("satoshi", email, "satoshi", 10, this._aggregateId));
    }

    [Theory]
    [InlineData("user@example.com")]
    [InlineData("user.name@example.com")]
    [InlineData("user_name@example.com")]
    [InlineData("user-name@example.com")]
    [InlineData("user+name@example.com")]
    [InlineData("user@example.co.uk")]
    [InlineData("user@example.org")]
    [InlineData("user@example.net")]
    [InlineData("user@example.io")]
    [InlineData("user@example.email")]
    [InlineData("user@example.travel")]
    [InlineData("user@example.museum")]
    [InlineData("user@example.photography")]
    [InlineData("user@subdomain.example.com")]
    [InlineData("user123@example.com")]
    [InlineData("123user@example.com")]
    [InlineData("user@example123.com")]
    [InlineData("user@123example.com")]
    public void IsValidEmail_VariousEmailReturnsInputEmail(string email)
    {
        //arrange & act
        var user = new UserEntity("satoshi", email, "satoshi", 10, this._aggregateId);

        //assert
        Assert.Equal(email, user.Email);
    }

    [Theory]
    [InlineData(-1)]
    public void IsValidAge_InvalidAgeThrowsException(int age)
    {
        //act & assert
        Assert.Throws<UserDomainException>(() => new UserEntity("satoshi", "satoshi@sample.com", "satoshi", age, this._aggregateId));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(10)]
    public void IsValidAge_VariousAgeReturnsInputAge(int age)
    {
        //arrange & act
        var user = new UserEntity("satoshi", "satoshi@sample.com", "satoshi", age, this._aggregateId);
        //assert
        Assert.Equal(age, user.Age);
    }
}