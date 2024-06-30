using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using User.Domain.exceptions;
using User.Domain.seedWork;

namespace User.domain.model;

/// <summary>
/// ユーザー情報
/// </summary>
public class UserEntity : Entity
{
    /// <summary>
    /// ユーザーID
    /// </summary>
    public long UserId { get; private set; }
    /// <summary>
    /// ユーザー名
    /// </summary>
    [Required]
    public string Name { get; private set; }

    /// <summary>
    /// メールアドレス
    /// </summary>
    [Required]
    public string Email { get; private set; }

    /// <summary>
    /// パスワード
    /// </summary>
    [Required]
    public string Password { get; private set; }

    /// <summary>
    /// 年齢
    /// </summary>
    [Required]
    public int Age { get; private set; }

    [Required]
    public Guid AggregateId { get; private set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="email">メールアドレス</param>
    /// <param name="password">パスワード</param>
    /// <param name="age">年齢</param>
    /// <param name="aggregateId">集約ID</param>
    public UserEntity(string name, string email, string password, int age, Guid aggregateId)
    {
        this.Name = name;
        this.Email = IsValidEmail(email);
        this.Password = password;
        this.Age = age;
        this.AggregateId = aggregateId;
    }

    public void UpdateDetails(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }

    // メールアドレスの形式チェックロジック
    // 例: 正規表現を使用した簡単なチェック
    private string IsValidEmail(string email)
    {
        var match = Regex.IsMatch(email, @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$");
        if (!match)
        {
            throw new UserDomainException("メールアドレスに使用できない文字が含まれています。");
        }

        return email;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public UserEntity() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}