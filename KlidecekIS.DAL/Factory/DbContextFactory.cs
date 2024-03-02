using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.Factory;

public class DbContextSqLiteFactory : IDbContextFactory<KliedecekDbContext>
{
    private readonly bool _seedTestingData;
    private readonly DbContextOptionsBuilder<KliedecekDbContext> _contextOptionsBuilder = new();

    public DbContextSqLiteFactory(string databaseName, bool seedTestingData = false)
    {
        _seedTestingData = seedTestingData;

        _contextOptionsBuilder.UseSqlite($"Data Source={databaseName};Cache=Shared");

    }

    public KliedecekDbContext CreateDbContext() => new(_contextOptionsBuilder.Options);
}