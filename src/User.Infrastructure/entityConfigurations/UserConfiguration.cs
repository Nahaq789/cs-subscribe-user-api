using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.domain.model;

namespace User.Infrastructure.entityConfigurations;

internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> userEntityConfiguration)
    {
        userEntityConfiguration.ToTable("user");
        userEntityConfiguration.HasKey(p => p.UserId);
        userEntityConfiguration.Property(p => p.UserId)
            .HasColumnName("user_id")
            .ValueGeneratedOnAdd();

        userEntityConfiguration.Property(e => e.Email)
            .HasColumnName("email")
            .HasMaxLength(30)
            .IsRequired();

        userEntityConfiguration.Property(e => e.Password)
            .HasColumnName("password")
            .HasMaxLength(20)
            .IsRequired();

        userEntityConfiguration.Property(e => e.Age)
            .HasColumnName("age")
            .IsRequired();
    }
}