using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.Factory;

public class DbContextSqLiteFactory : IDbContextFactory<KlidecekDbContext>
{
    private readonly bool _seedTestingData;
    private readonly DbContextOptionsBuilder<KlidecekDbContext> _contextOptionsBuilder = new();

    public DbContextSqLiteFactory(string databaseName, bool seedTestingData = true)
    {
        _seedTestingData = seedTestingData;

        _contextOptionsBuilder.UseSqlite($"Data Source={databaseName};Cache=Shared");

    }

    public KlidecekDbContext CreateDbContext()
    {
        // _contextOptionsBuilder.LogTo(Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        // _contextOptionsBuilder.EnableSensitiveDataLogging(); 
        return new KlidecekDbContext(_contextOptionsBuilder.Options);
    }
}