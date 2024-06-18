using User.Domain.seedWork;

namespace User.domain.model;

/// <summary>
/// ユーザーリポジトリです。
/// </summary>
public interface IUserRepository : IRepository<UserAggregate>
{
    Task CreateUser(UserAggregate userEntity);
    void UpdateUser(UserAggregate userEntity);
}