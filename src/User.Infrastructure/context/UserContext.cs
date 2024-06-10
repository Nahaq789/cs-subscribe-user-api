using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using User.domain.model;
using User.Infrastructure.entityConfigurations;

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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserSaltConfiguration());
        builder.ApplyConfiguration(new UserAggregateConfiguration());
        base.OnModelCreating(builder);
    }
}