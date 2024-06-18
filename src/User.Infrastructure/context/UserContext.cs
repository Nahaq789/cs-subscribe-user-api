using System.Data.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using User.domain.model;
using User.Domain.seedWork;
using User.Infrastructure;
using User.Infrastructure.entityConfigurations;

namespace User.API.infrastructure;

public class UserContext : DbContext, IUnitOfWork
{
    public DbSet<UserAggregate> UserAggregates { get; set; }
    public DbSet<UserEntity> User { get; set; }
    public DbSet<UserSalt> UserSalts { get; set; }

    private readonly IMediator _mediator;
    //public UserContext(DbContextOptions<UserContext> options) : base(options) { }
    public UserContext(DbContextOptions<UserContext> options, IMediator mediator) : base(options)
    {
        this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.HasDefaultSchema("users");
        // base.OnModelCreating(builder);
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserSaltConfiguration());
        builder.ApplyConfiguration(new UserAggregateConfiguration());
    }

    public async Task<bool> SaveEntityAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);
        _ = await base.SaveChangesAsync(cancellationToken);
        return true;
    }
}