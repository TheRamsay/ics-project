using KlidecekIS.DAL;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.Common.Tests.Factories;

public class DbContextSQLiteTestingFactory(string databaseName, bool seedTestingData = false): IDbContextFactory<KlidecekDbContext>
{
    public KlidecekDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<KlidecekDbContext> builder = new();
        
        
        
        builder.UseSqlite($"Data Source={databaseName};Cache=Shared");
        
        builder.LogTo(Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        builder.EnableSensitiveDataLogging(); 
        
        return new KlidecekTestingDbContext(builder.Options, seedTestingData);
    }
}