using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.Mappers;
using KlidecekIS.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.UnitOfWork;

public sealed class UnitOfWork(DbContext dbContext): IUnitOfWork
{
    private readonly DbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

    public async Task CommitAsync() => await _dbContext.SaveChangesAsync();
    
    public IRepository<TEntity> GetRepository<TEntity>() 
        where TEntity : class, IEntity 
    {
        return new Repository<TEntity>(_dbContext);
    }

}