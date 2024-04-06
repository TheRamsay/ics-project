using KlidecekIS.DAL.Entities;

namespace KlidecekIS.DAL.Mappers;

public interface IEntityMapper<in TEntity> where TEntity : IEntity
{
    void MapToExistingEntity(TEntity existingEntity, TEntity newEntity);
}