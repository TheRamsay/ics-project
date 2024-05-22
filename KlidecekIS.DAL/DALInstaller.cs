using KlidecekIS.DAL.Factory;
using KlidecekIS.DAL.Migrator;
using KlidecekIS.DAL.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KlidecekIS.DAL;

public static class DALInstaller
{
    public static IServiceCollection AddDALServices(this IServiceCollection services,DalOptions options)
    {
        services.AddSingleton(options);

        if (options is null)
        {
            throw new InvalidOperationException("No persistence provider configured");
        }

        if (string.IsNullOrEmpty(options.DatabaseDirectory))
        {
            throw new InvalidOperationException($"{nameof(options.DatabaseDirectory)} is not set");
        }
        if (string.IsNullOrEmpty(options.DatabaseName))
        {
            throw new InvalidOperationException($"{nameof(options.DatabaseName)} is not set");
        }

        services.AddSingleton<IDbContextFactory<KlidecekDbContext>>(_ =>
            new DbContextSqLiteFactory(options.DatabaseFilePath, options?.SeedDemoData ?? false));
        services.AddSingleton<IDbMigrator, DbMigrator>();

        return services;
    }
}