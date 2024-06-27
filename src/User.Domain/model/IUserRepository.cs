using User.Domain.seedWork;

namespace User.domain.model;

/// <summary>
/// ユーザーリポジトリです。
/// </summary>
public interface IUserRepository : IRepository<UserAggregate>
{
    Task<UserAggregate> FindUserByAggregateId(Guid userAggregateId);
    Task CreateAsync(UserAggregate user);
    void Update(UserAggregate user);
}