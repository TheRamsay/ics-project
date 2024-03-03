using KlidecekIS.Common.Tests;
using KlidecekIS.Common.Tests.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit.Abstractions;

namespace KlidecekIS.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    public DbContextTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);
        KlidecekDbContextSUT = DbContextFactory.CreateDbContext();
    }
    
    protected IDbContextFactory<KlidecekDbContext> DbContextFactory { get; }
    protected KlidecekDbContext KlidecekDbContextSUT { get; }
    
    public async Task InitializeAsync()
    {
        await KlidecekDbContextSUT.Database.EnsureDeletedAsync();
        await KlidecekDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await KlidecekDbContextSUT.Database.EnsureDeletedAsync();
        await KlidecekDbContextSUT.DisposeAsync();
    }
}