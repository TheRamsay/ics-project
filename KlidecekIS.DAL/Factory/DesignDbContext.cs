using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KlidecekIS.DAL.Factory;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KlidecekDbContext>
{
    private readonly DbContextSqLiteFactory _dbContextSqLiteFactory = new($"Data Source=KlidecekDb;Cache=Shared");

    public KlidecekDbContext CreateDbContext(string[] args) => _dbContextSqLiteFactory.CreateDbContext();
}