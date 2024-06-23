using System.Data.Common;
using System.Diagnostics;
using System.Net.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace User.API.extensions;

internal static class MigrationDbContextExtensions
{
    private static readonly string ActivitySourceName = "DbMigrations";
    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    public static IServiceCollection AddMigration<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        return services.AddHostedService(sp => new MigrationHostedService<TContext>(sp));
    }

    private static async Task MigrateDbContextAsync<TContext>(this IServiceProvider services) where TContext : DbContext
    {
        using var scope = services.CreateScope();
        var scopeServices = scope.ServiceProvider;
        var logger = scopeServices.GetRequiredService<ILogger<TContext>>();
        var context = scopeServices.GetService<TContext>();

        using var activity = ActivitySource.StartActivity($"Migration operation {typeof(TContext).Name}");

        try
        {
            logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);
            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(() => Invoke<TContext>(context, scopeServices));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
            throw;
        }
    }

    private static async Task Invoke<TContext>(TContext context, IServiceProvider services) where TContext : DbContext
    {
        using var activity = ActivitySource.StartActivity($"Migrating {typeof(TContext).Name}");
        try
        {
            // if (!context.Database.GetAppliedMigrations().Any())
            // {
            //     await context.Database.MigrateAsync();
            // }
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private class MigrationHostedService<TContext>(IServiceProvider serviceProvider) : BackgroundService where TContext : DbContext
    {
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return serviceProvider.MigrateDbContextAsync<TContext>();
        }
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}