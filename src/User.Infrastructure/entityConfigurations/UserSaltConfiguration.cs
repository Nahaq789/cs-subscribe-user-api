using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.domain.model;

namespace User.Infrastructure.entityConfigurations;

internal class UserSaltConfiguration : IEntityTypeConfiguration<UserSalt>
{
    public void Configure(EntityTypeBuilder<UserSalt> userSaltConfiguration)
    {
        userSaltConfiguration.ToTable("user_salt");
        userSaltConfiguration.Property(u => u.Id).UseHiLo("userseq");
        userSaltConfiguration.Ignore(b => b.DomainEvents);
        
        userSaltConfiguration.Property(e => e.Salt)
            .HasColumnName("salt");
    }
}