using Microsoft.EntityFrameworkCore;
using User.API.application.service;
using User.API.infrastructure;
using User.domain.model;

namespace User.API.extensions;

public static class Extensions
{
    public static void StartupDI(this WebApplicationBuilder builder)
    {
        var configure = builder.Configuration;
        var connectionString = configure.GetConnectionString("userdb");

        builder.Services.AddDbContextPool<UserContext>((serviceProvider, option) =>
        {
            option.UseNpgsql(connectionString);
        });

        var services = builder.Services;
        services.AddMediatR((cfg) =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
        });

        builder.Services.AddTransient<ICryptoPasswordService, CryptoPasswordService>();
        builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();

    }
}