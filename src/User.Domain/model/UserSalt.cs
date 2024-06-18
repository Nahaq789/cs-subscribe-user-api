using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using User.Domain.seedWork;

namespace User.domain.model;

/// <summary>
/// パスワードハッシュ化に使用するソルトを管理するユーザーのエントリを表します。
/// </summary>
public class UserSalt : Entity
{
    public Guid SaltId { get; private set; }
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
    public UserSalt(Guid saltId, string salt, Guid aggregateId)
    {
        this.SaltId = saltId;
        this.Salt = salt;
        this.AggregateId = aggregateId;
    }

    public UserSalt() { }
}

