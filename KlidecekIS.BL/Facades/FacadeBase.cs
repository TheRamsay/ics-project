using System.Collections;
using System.Linq.Expressions;
using AutoMapper;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.BL.Facades;

public abstract class
    FacadeBase<TEntity, TListModel, TDetailModel>(
        IMapper modelMapper,
        IUnitOfWorkFactory unitOfWorkFactory)
    : IFacade<TEntity, TListModel, TDetailModel>
    where TEntity : class, IEntity
    where TListModel : IModel 
    where TDetailModel : class, IModel
{
    protected readonly IMapper ModelMapper = modelMapper;
    protected readonly IUnitOfWorkFactory UnitOfWorkFactory = unitOfWorkFactory;

    protected virtual IEnumerable<string> IncludesNavigationPathDetail => new List<string>();

    public async Task DeleteAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        try
        {
            await uow.GetRepository<TEntity>().DeleteAsync(id).ConfigureAwait(false);
            await uow.CommitAsync().ConfigureAwait(false);
        }
        catch (DbUpdateException e)
        {
            throw new InvalidOperationException("Entity deletion failed.", e);
        }
    }

    public virtual async Task<TDetailModel?> GetAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();

        var query = uow.GetRepository<TEntity>().Get();

        foreach (var includePath in IncludesNavigationPathDetail)
        {
            query = query.Include(includePath);
        }

        var entity = await query.SingleOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);

        return entity is null
            ? null
            : ModelMapper.Map<TDetailModel>(entity);
    }

    // Always use paging in production
    public virtual async Task<IEnumerable<TListModel>> GetAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();
        var entities = await uow
            .GetRepository<TEntity>()
            .Get()
            .ToListAsync().ConfigureAwait(false);

        return ModelMapper.Map<List<TListModel>>(entities);
    }
    
    public virtual async Task<IEnumerable<TListModel>> SortBy(Expression<Func<TEntity, object>> keySelector, bool ascending)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var entities = uow
            .GetRepository<TEntity>()
            .Get();

        var sortedEntities = ascending ? entities.OrderBy(keySelector) : entities.OrderByDescending(keySelector);
        
        return ModelMapper.Map<List<TListModel>>(await sortedEntities.ToListAsync());
    }

    public virtual async Task<TDetailModel> SaveAsync(TDetailModel model)
    {
        TDetailModel result;

        GuardCollectionsAreNotSet(model);

        var entity = ModelMapper.Map<TEntity>(model);

        var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<TEntity>();

        if (await repository.ExistsAsync(entity).ConfigureAwait(false))
        {
            var updatedEntity = await repository.UpdateAsync(entity).ConfigureAwait(false);
            result = ModelMapper.Map<TDetailModel>(updatedEntity);
        }
        else
        {
            entity.Id = Guid.NewGuid();
            var insertedEntity = repository.Insert(entity);
            result = ModelMapper.Map<TDetailModel>(insertedEntity);
        }

        await uow.CommitAsync().ConfigureAwait(false);

        return result;
    }

    /// <summary>
    /// This Guard ensures that there is a clear understanding of current infrastructure limitations.
    /// This version of BL/DAL infrastructure does not support insertion or update of adjacent entities.
    /// WARN: Does not guard navigation properties.
    /// </summary>
    /// <param name="model">Model to be inserted or updated</param>
    /// <exception cref="InvalidOperationException"></exception>
    private static void GuardCollectionsAreNotSet(TDetailModel model)
    {
        var collectionProperties = model
            .GetType()
            .GetProperties()
            .Where(i => typeof(ICollection).IsAssignableFrom(i.PropertyType));

        if (collectionProperties.Any(collectionProperty => collectionProperty.GetValue(model) is ICollection { Count: > 0 }))
        {
            throw new InvalidOperationException(
                "Current BL and DAL infrastructure disallows insert or update of models with adjacent collections.");
        }
    }
}