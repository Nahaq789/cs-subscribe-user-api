using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.domain.model;
using YourNamespace;

namespace User.Infrastructure.entityConfigurations;

public class UserAggregateConfiguration : IEntityTypeConfiguration<UserAggregate>
{
    public void Configure(EntityTypeBuilder<UserAggregate> userAggregateConfiguration)
    {
        userAggregateConfiguration.ToTable("user_aggregate");
        userAggregateConfiguration.HasKey(p => p.UserAggregateId);
        userAggregateConfiguration.Property(p => p.UserAggregateId)
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd()
            .HasColumnName("user_aggregate_id");

        userAggregateConfiguration.HasOne<UserEntity>()
            .WithOne()
            .HasForeignKey<UserAggregate>(f => f.UserId)
            .IsRequired();

        userAggregateConfiguration.Property(f => f.UserId)
            .HasColumnName("user_id");

        userAggregateConfiguration.HasOne<UserSalt>()
            .WithOne()
            .HasForeignKey<UserAggregate>(f => f.SaltId)
            .IsRequired();

        userAggregateConfiguration.Property(f => f.SaltId)
            .HasColumnName("salt_id");

        userAggregateConfiguration.Ignore(e => e.User);
        userAggregateConfiguration.Ignore(e => e.Salt);
    }
}