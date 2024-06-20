using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.domain.model;

namespace User.Infrastructure.entityConfigurations;

internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> userEntityConfiguration)
    {
        userEntityConfiguration.ToTable("user");
        //userEntityConfiguration.Property(u => u.Id).UseHiLo("userseq");
        userEntityConfiguration.Ignore(b => b.DomainEvents);

        userEntityConfiguration.Property(p => p.UserId)
            .HasColumnName("user_id")
            .ValueGeneratedOnAdd()
            .IsRequired();
        userEntityConfiguration.HasKey(p => p.UserId);

        userEntityConfiguration.Property(e => e.Email)
            .HasColumnName("email")
            .HasMaxLength(30)
            .IsRequired();

        userEntityConfiguration.Property(e => e.Password)
            .HasColumnName("password")
            .HasMaxLength(50)
            .IsRequired();

        userEntityConfiguration.Property(e => e.Age)
            .HasColumnName("age")
            .IsRequired();

        userEntityConfiguration.Property(e => e.AggregateId)
            .HasColumnName("user_aggregate_id")
            .IsRequired();
        userEntityConfiguration.HasOne<UserAggregate>()
            .WithOne()
            .HasForeignKey<UserEntity>(f => f.AggregateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}