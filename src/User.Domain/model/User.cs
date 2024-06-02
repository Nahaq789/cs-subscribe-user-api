using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User.domain.model;

[Table("User")]
public class UserEntity
{
    /// <summary>
    /// 主キー ユーザーID
    /// </summary>
    [Key]
    public long UserId { get; private set; }

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
    /// コンストラクタ
    /// </summary>
    /// <param name="userId">ユーザーID</param>
    /// <param name="email">メールアドレス</param>
    /// <param name="password">パスワード</param>
    public UserEntity(long userId, string email, string password)
    {
        this.UserId = userId;
        this.Email = email;
        this.Password = password;
    }
}