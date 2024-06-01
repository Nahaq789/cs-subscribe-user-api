using Microsoft.EntityFrameworkCore;

using User.API.domain;

namespace User.API.infrastructure;

public class UserContext : DbContext
{
    public DbSet<UserEntity> User { get; set; }

    public UserContext() { }
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {

    }
}