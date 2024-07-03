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
        this.Age = IsValidAge(age);
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
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new UserDomainException("メールアドレスが空です。");
        }

        // 基本的な形式チェック
        var regex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", RegexOptions.None, TimeSpan.FromMilliseconds(250));
        if (!regex.IsMatch(email))
        {
            throw new UserDomainException("メールアドレスの形式が正しくありません。");
        }

        // ローカル部分と ドメイン部分に分割
        var parts = email.Split('@');
        var local = parts[0];
        var domain = parts[1];

        // ローカル部分のチェック
        if (local.Length > 64)
        {
            throw new UserDomainException("ローカル部分が64文字を超えています。");
        }

        if (local.StartsWith(".") || local.EndsWith(".") || local.Contains(".."))
        {
            throw new UserDomainException("ローカル部分に不正なドットの使用があります。");
        }

        // ドメイン部分のチェック
        if (domain.StartsWith("-") || domain.EndsWith("-"))
        {
            throw new UserDomainException("ドメイン名はハイフンで始まったり終わったりすることはできません。");
        }

        if (domain.Contains("_"))
        {
            throw new UserDomainException("ドメイン名にアンダースコアは使用できません。");
        }

        if (domain.Contains("-"))
        {
            throw new UserDomainException("ドメイン名に「-」を含むことはできません。");
        }

        // 各ドメイン名は2文字以上63文字以下
        var domainParts = domain.Split('.');
        if (domainParts.Any(p => p.Length > 63) || domainParts.Any(p => 2 > p.Length))
        {
            throw new UserDomainException("ドメイン名の各部分は2文字以上63文字以下である必要があります。");
        }


        if (domainParts.Length < 2)
        {
            throw new UserDomainException("ドメイン名は少なくとも1つのドットを含む必要があります。");
        }

        return email;
    }

    private int IsValidAge(int age)
    {
        if (age < 0)
        {
            throw new UserDomainException("年齢に負の値を適用することはできません");
        }

        return age;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public UserEntity() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}