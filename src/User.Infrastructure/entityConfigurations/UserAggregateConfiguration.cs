using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.domain.model;

namespace User.Infrastructure.entityConfigurations;

internal class UserAggregateConfiguration : IEntityTypeConfiguration<UserAggregate>
{
    public void Configure(EntityTypeBuilder<UserAggregate> userAggregateConfiguration)
    {
        userAggregateConfiguration.ToTable("user_aggregate");
        userAggregateConfiguration.Property(u => u.Id)
            .UseHiLo("userseq");
        userAggregateConfiguration.Ignore(b => b.DomainEvents);

        userAggregateConfiguration.Property(p => p.UserAggregateId)
            .ValueGeneratedOnAdd()
            .HasColumnName("user_aggregate_id");
    }
}