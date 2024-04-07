using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.Repositories;

namespace KlidecekIS.DAL.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class, IEntity;
    
    Task CommitAsync();
}