using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.domain.model;

namespace User.Infrastructure.entityConfigurations;

internal class UserAggregateConfiguration : IEntityTypeConfiguration<UserAggregate>
{
    public void Configure(EntityTypeBuilder<UserAggregate> userAggregateConfiguration)
    {
        userAggregateConfiguration.ToTable("user_aggregate");
        // userAggregateConfiguration.Property(u => u.Id)
        //     .UseHiLo("user_aggregate_ßseq");
        userAggregateConfiguration.Ignore(b => b.DomainEvents);

        userAggregateConfiguration.Property(p => p.UserAggregateId)
            .ValueGeneratedOnAdd()
            .HasColumnName("user_aggregate_id");

        userAggregateConfiguration.HasKey(p => p.UserAggregateId);
        userAggregateConfiguration
            .HasOne(p => p.User)
            .WithOne()
            .HasForeignKey<UserEntity>(p => p.AggregateId);

        // UserSaltの設定
        userAggregateConfiguration
            .HasOne(p => p.Salt)
            .WithOne()
            .HasForeignKey<UserSalt>(p => p.AggregateId);
    }
}