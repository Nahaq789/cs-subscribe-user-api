using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using User.API.application.service;
using User.API.infrastructure;
using User.domain.model;
using User.Infrastructure.repository;

namespace User.API.extensions;

public static class Extensions
{
    public static void StartupDI(this WebApplicationBuilder builder)
    {
        var configure = builder.Configuration;
        var connectionString = configure.GetConnectionString("userdb");

        builder.Services.AddDbContextPool<UserContext>((serviceProvider, option) =>
        {
            option.UseNpgsql(connectionString, (m) => m.MigrationsAssembly("User.Infrastructure"));
        });

        builder.Services.AddMigration<UserContext>();

        var services = builder.Services;
        services.AddMediatR((cfg) =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
        });

        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<ICryptoPasswordService, CryptoPasswordService>();
        builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();

    }
}