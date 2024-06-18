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


    public async Task CreateUser(UserAggregate userEntity)
    {
        await _userContext.UserAggregates.AddAsync(userEntity);
    }

    public void UpdateUser(UserAggregate userEntity)
    {
        //_userContext.User.Attach(userEntity);
        _userContext.Entry(userEntity).State = EntityState.Modified;
    }
}