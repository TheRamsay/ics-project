using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.Mappers;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.Repositories;

public class Repository<TEntity>(
    DbContext dbContext) 
    : IRepository<TEntity> 
    where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    public IQueryable<TEntity> Get() => _dbSet;

    public async Task DeleteAsync(Guid entityGuid)
    {
        _dbSet.Remove(await _dbSet.SingleAsync(i => i.Id == entityGuid));
    }

    public async ValueTask<bool> ExistsAsync(TEntity entity)
    {
        if (entity.Id == Guid.Empty)
        {
            return false;
        }

        return await _dbSet.AnyAsync(e => e.Id == entity.Id);
    }

    public TEntity Insert(TEntity entity) => _dbSet.Add(entity).Entity;
    
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var existingEntity = await _dbSet.SingleAsync(i => i.Id == entity.Id);

        // Update all properties of the existing entity with the new entity's properties
        // Using reflection to avoid writing a lot of boilerplate code
        // Idk if this legit, but it works (we pray) 😵‍💫
        foreach (var property in entity.GetType().GetProperties())
        {
            var value = property.GetValue(entity);
            property.SetValue(existingEntity, value);
        }
        
        return existingEntity;
    }
}