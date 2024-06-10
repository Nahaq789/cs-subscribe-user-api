using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.domain.model;

namespace User.Infrastructure.entityConfigurations;

public class UserSaltConfiguration : IEntityTypeConfiguration<UserSalt>
{
    public void Configure(EntityTypeBuilder<UserSalt> userSaltConfiguration)
    {
        userSaltConfiguration.ToTable("user_salt");
        userSaltConfiguration.HasKey(p => p.SaltId);
        userSaltConfiguration.Property(p => p.SaltId)
            .HasColumnName("salt_id")
            .ValueGeneratedOnAdd();

        userSaltConfiguration.HasOne<UserAggregate>()
            .WithOne()
            .HasForeignKey<UserSalt>(p => p.UserAggregateId)
            .IsRequired();

        userSaltConfiguration.Property(e => e.UserAggregateId)
            .HasColumnName("user_aggregate_id");

        userSaltConfiguration.Property(e => e.Salt)
            .HasColumnName("salt");
    }
}