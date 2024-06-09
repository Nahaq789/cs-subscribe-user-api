using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourNamespace;

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

        userSaltConfiguration.


    }
}