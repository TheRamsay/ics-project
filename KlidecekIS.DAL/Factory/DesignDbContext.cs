using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KlidecekIS.DAL.Factory;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KliedecekDbContext>
{
    private readonly DbContextSqLiteFactory _dbContextSqLiteFactory = new($"Data Source=KlidecekDb;Cache=Shared");

    public KliedecekDbContext CreateDbContext(string[] args) => _dbContextSqLiteFactory.CreateDbContext();
}