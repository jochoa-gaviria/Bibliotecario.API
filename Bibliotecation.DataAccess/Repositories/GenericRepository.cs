using Bibliotecario.DataAccess.Contracts.Interfaces;
using Bibliotecario.DataAccess.Context;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Bibliotecario.DataAccess.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    #region internals
    private readonly PersistenceContext _databaseContext;
    #endregion internals

    #region constructor
    public GenericRepository(PersistenceContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    #endregion constructor

    #region methods
    public async Task<bool> Delete(TEntity entity)
    {
        try
        {
            _databaseContext.Set<TEntity>().Remove(entity);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteRange(List<TEntity> entities)
    {
        try
        {
            _databaseContext.Set<TEntity>().RemoveRange(entities);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public async Task<TEntity> Get(Expression<Func<TEntity, bool>> expression)
    {
        return await _databaseContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(expression);
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await _databaseContext.Set<TEntity>().ToListAsync();
    }

    public async Task<int> Count(Expression<Func<TEntity, bool>> expression)
    {
        return await _databaseContext.Set<TEntity>().AsNoTracking().CountAsync(expression);
    }

    public async Task<TEntity?> New(TEntity entity)
    {
        try
        {
            _databaseContext.Set<TEntity>().Add(entity);
            await _databaseContext.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    public async Task<List<TEntity>?> NewRange(List<TEntity> entities)
    {
        try
        {
            await _databaseContext.Set<TEntity>().AddRangeAsync(entities);
            await _databaseContext.SaveChangesAsync();
            return entities;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> Update(TEntity entity)
    {
        try
        {
            _databaseContext.Set<TEntity>().Update(entity);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> UpdateRange(List<TEntity> entities)
    {
        try
        {
            _databaseContext.Set<TEntity>().UpdateRange(entities);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
    #endregion methods

}
