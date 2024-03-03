using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.Factory;

public class DbContextSqLiteFactory : IDbContextFactory<KlidecekDbContext>
{
    private readonly bool _seedTestingData;
    private readonly DbContextOptionsBuilder<KlidecekDbContext> _contextOptionsBuilder = new();

    public DbContextSqLiteFactory(string databaseName, bool seedTestingData = false)
    {
        _seedTestingData = seedTestingData;

        _contextOptionsBuilder.UseSqlite($"Data Source={databaseName};Cache=Shared");

    }

    public KlidecekDbContext CreateDbContext() => new(_contextOptionsBuilder.Options);
}