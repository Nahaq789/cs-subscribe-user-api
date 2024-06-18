using System.ComponentModel.DataAnnotations.Schema;
using User.Domain.seedWork;

namespace User.domain.model;

/// <summary>
/// ユーザ集約クラス
/// </summary>
public class UserAggregate : Entity, IAggregateRoot
{
    /// <summary>
    /// ユーザー集約ID
    /// </summary>
    public Guid UserAggregateId { get; private set; }

    /// <summary>
    /// ユーザー
    /// </summary>
    private UserEntity _user;
    public UserEntity User => _user;

    /// <summary>
    /// ソルト
    /// </summary>
    private UserSalt _salt;
    public UserSalt Salt => _salt;

    public UserAggregate(Guid userAggregateId) : this()
    {
        this.UserAggregateId = userAggregateId;
    }
    protected UserAggregate()
    {
        this._user = new UserEntity();
        this._salt = new UserSalt();
    }

    public void setUser(Guid userId, string name, string email, string password, int age, Guid aggregateId)
    {
        var user = new UserEntity(userId, name, email, password, age, aggregateId);
        this._user = user;
    }
    public void setSalt(Guid saltId, string Salt, Guid aggregateId)
    {
        var salt = new UserSalt(saltId, Salt, aggregateId);
        this._salt = salt;
    }

    // マイグレーション時のみコメントアウト外す
    //public UserAggregate() { }
}