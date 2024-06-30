using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using User.API.behaviors;

namespace User.API.extensions;

public static class AddJwtExtensions
{
    public static IServiceCollection AddJwtBehavior(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        var builder = new AuthenticationBuilder(services);
        JwtTokenBehavior.JwtBehavior(builder, configuration);

        return services;
    }
}
