using Microsoft.EntityFrameworkCore;
using User.API.infrastructure;
using User.domain.model;

namespace User.Infrastructure.repository;

public class UserRepository : IUserRepository
{
    private readonly UserContext _userContext;

    public UserRepository(UserContext userContext) => this._userContext = userContext;

    public async Task CreateUser(UserEntity userEntity)
    {
        _userContext.User.Add(userEntity);
        await _userContext.SaveChangesAsync();
    }

    public async Task UpdateUser(UserEntity userEntity)
    {
        _userContext.User.Attach(userEntity);
        _userContext.Entry(userEntity).State = EntityState.Modified;
        await _userContext.SaveChangesAsync();
    }
}