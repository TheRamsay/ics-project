using KlidecekIS.DAL;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.Common.Tests.Factories;

public class DbContextSQLiteTestingFactory(string databaseName, bool seedTestingData = false): IDbContextFactory<KlidecekDbContext>
{
    public KlidecekDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<KlidecekDbContext> builder = new();
        
        builder.UseSqlite($"Data Source={databaseName};Cache=Shared");
        
        return new KlidecekTestingDbContext(builder.Options, seedTestingData);
    }
}