using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using User.domain.model;
using YourNamespace;
namespace User.API.infrastructure;

public class UserContext : DbContext
{
    public DbSet<UserAggregate> UserAggregates { get; set; }
    public DbSet<UserEntity> User { get; set; }
    public DbSet<UserSalt> userSalts { get; set; }

    public UserContext() { }
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {

    }
}