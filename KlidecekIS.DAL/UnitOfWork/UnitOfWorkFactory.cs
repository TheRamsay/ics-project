using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.UnitOfWork;

public class UnitOfWorkFactory(IDbContextFactory<KlidecekDbContext> dbContextFactory) : IUnitOfWorkFactory
{
    public IUnitOfWork Create() => new UnitOfWork(dbContextFactory.CreateDbContext());
}