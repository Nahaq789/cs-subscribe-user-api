using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using User.Domain.seedWork;

namespace User.domain.model;

/// <summary>
/// パスワードハッシュ化に使用するソルトを管理するユーザーのエントリを表します。
/// </summary>
public class UserSalt : Entity
{
    /// <summary>
    /// ソルトID
    /// </summary>
    public long SaltId { get; private set; }
    /// <summary>
    /// パスワードハッシュ化に使用するソルト文字列を取得します。
    /// </summary>
    [Required]
    public string Salt { get; private set; }

    /// <summary> 
    /// ユーザー集約IDです。
    /// </summary>
    [Required]
    public Guid AggregateId { get; private set; }

    /// <summary>
    /// <see cref="UserSalt"/> コンストラクタ
    /// </summary>
    /// <param name="salt">パスワードハッシュ化に使用するソルト文字列。</param>
    /// <param name="aggregateId">ユーザー集約ID</param>
    public UserSalt(string salt, Guid aggregateId)
    {
        this.Salt = salt;
        this.AggregateId = aggregateId;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public UserSalt() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}

