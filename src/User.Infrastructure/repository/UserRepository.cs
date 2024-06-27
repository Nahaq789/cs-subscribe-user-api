using Microsoft.EntityFrameworkCore;
using User.API.infrastructure;
using User.domain.model;
using User.Domain.seedWork;

namespace User.Infrastructure.repository;

public class UserRepository : IUserRepository
{
    private readonly UserContext _userContext;
    public IUnitOfWork UnitOfWork => _userContext;

    public UserRepository(UserContext userContext) => this._userContext = userContext;

    public async Task<UserAggregate> FindUserByAggregateId(Guid aggregateId)
    {
        var result = await _userContext.UserAggregates
            .Include(e => e.User)
            .Include(e => e.Salt)
            .FirstOrDefaultAsync(p => p.UserAggregateId == aggregateId);
#pragma warning disable CS8603 // Possible null reference return.
        return result;
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task CreateAsync(UserAggregate user)
    {
        await _userContext.UserAggregates.AddAsync(user);
    }

    public void Update(UserAggregate user)
    {
        _userContext.UserAggregates.Update(user);
    }
}