using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using User.domain.model;
namespace User.API.infrastructure;

public class UserContext : DbContext
{
    public DbSet<UserEntity> User { get; set; }

    public UserContext() { }
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {

    }
}