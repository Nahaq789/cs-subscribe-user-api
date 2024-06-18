using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.domain.model;

namespace User.Infrastructure.entityConfigurations;

internal class UserSaltConfiguration : IEntityTypeConfiguration<UserSalt>
{
    public void Configure(EntityTypeBuilder<UserSalt> userSaltConfiguration)
    {
        userSaltConfiguration.ToTable("user_salt");
        //userSaltConfiguration.Property(u => u.Id).UseHiLo("user_salt_seq");
        userSaltConfiguration.Ignore(b => b.DomainEvents);

        userSaltConfiguration.HasKey(p => p.SaltId);

        userSaltConfiguration.Property(p => p.SaltId)
            .HasColumnName("salt_id")
            .ValueGeneratedOnAdd()
            .IsRequired();
        userSaltConfiguration.Property(e => e.Salt)
            .HasColumnName("salt")
            .IsRequired();

        userSaltConfiguration.Property(e => e.AggregateId)
            .HasColumnName("user_aggregate_id")
            .IsRequired();
        userSaltConfiguration.HasOne<UserAggregate>()
            .WithOne()
            .HasForeignKey<UserSalt>(f => f.AggregateId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}