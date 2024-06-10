using System.ComponentModel.DataAnnotations.Schema;
using YourNamespace;

namespace User.domain.model;

/// <summary>
/// ユーザ集約クラス
/// </summary>
public class UserAggregate
{
    /// <summary>
    /// ユーザー集約ID
    /// </summary>
    public Guid UserAggregateId { get; private set; }

    /// <summary>
    /// ユーザーID
    /// </summary>
    public long UserId { get; private set; }

    /// <summary>
    /// ソルトID
    /// </summary>
    public long SaltId { get; private set; }

    /// <summary>
    /// ユーザー情報
    /// </summary>
    public UserEntity User { get; private set; }

    /// <summary>
    /// ユーザーのソルト情報
    /// </summary>
    public UserSalt Salt { get; private set; }

    /// <summary>
    /// <see cref="UserAggregate"/> コンストラクタ
    /// </summary>
    /// <param name="userAggregateId">ユーザー集約ID</param>
    /// <param name="userId">ユーザーID</param>
    /// <param name="user">ユーザー情報</param>
    /// <param name="salt">ユーザーのソルト情報</param>
    public UserAggregate(Guid userAggregateId, long userId, long saltId, UserEntity user, UserSalt salt)
    {
        this.UserAggregateId = userAggregateId;
        this.UserId = userId;
        this.SaltId = saltId;
        this.User = user;
        this.Salt = salt;
    }
}