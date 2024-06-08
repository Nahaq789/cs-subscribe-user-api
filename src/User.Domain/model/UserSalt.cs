using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourNamespace
{
    /// <summary>
    /// パスワードハッシュ化に使用するソルトを管理するユーザーのエントリを表します。
    /// </summary>
    [Table("Salt")]
    public class UserSalt
    {
        /// <summary>
        /// ソルトエントリの一意の識別子を取得します。
        /// </summary>
        [Key]
        public long SaltId { get; private set; }

        /// <summary>
        /// 関連付けられたユーザーの一意の識別子を取得します。
        /// </summary>
        [Required]
        public Guid UserId { get; private set; }

        /// <summary>
        /// パスワードハッシュ化に使用するソルト文字列を取得します。
        /// </summary>
        [Required]
        public string Salt { get; private set; }

        /// <summary>
        /// <see cref="UserSalt"/> コンストラクタ
        /// </summary>
        /// <param name="saltId">ソルトエントリの一意の識別子。</param>
        /// <param name="userId">関連付けられたユーザーの一意の識別子。</param>
        /// <param name="salt">パスワードハッシュ化に使用するソルト文字列。</param>
        public UserSalt(long saltId, Guid userId, string salt)
        {
            this.SaltId = saltId;
            this.UserId = userId;
            this.Salt = salt;
        }
    }
}
