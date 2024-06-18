using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using User.Domain.seedWork;

namespace User.domain.model;

/// <summary>
/// ユーザー情報
/// </summary>
public class UserEntity : Entity
{
    public Guid UserId { get; private set; }
    /// <summary>
    /// ユーザー名
    /// </summary>
    [Required]
    [StringLength(20)]
    public string Name { get; private set; }

    /// <summary>
    /// メールアドレス
    /// </summary>
    [Required]
    [StringLength(30)]
    public string Email { get; private set; }

    /// <summary>
    /// パスワード
    /// </summary>
    [Required]
    [StringLength(20)]
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
    public UserEntity(Guid userId, string name, string email, string password, int age, Guid aggregateId)
    {
        this.UserId = userId;
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.Age = age;
        this.AggregateId = aggregateId;
    }

    public UserEntity() { }
}