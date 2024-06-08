using System.ComponentModel.DataAnnotations.Schema;
using YourNamespace;

namespace User.domain.model;

/// <summary>
/// ユーザ集約クラス
/// </summary>
[Table("UserAggregate")]
public class UserAggregate
{
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
    /// <param name="user">ユーザー情報。</param>
    /// <param name="salt">ユーザーのソルト情報。</param>
    public UserAggregate(UserEntity user, UserSalt salt)
    {
        this.User = user;
        this.Salt = salt;
    }
}