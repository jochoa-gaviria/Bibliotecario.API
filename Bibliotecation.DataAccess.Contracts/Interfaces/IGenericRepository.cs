using System.Linq.Expressions;

namespace Bibliotecario.DataAccess.Contracts.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAll();
    Task<TEntity> Get(Expression<Func<TEntity, bool>> expression);
    Task<int> Count(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> New(TEntity entity);
    Task<List<TEntity>> NewRange(List<TEntity> entities);
    Task<bool> Update(TEntity entity);
    Task<bool> UpdateRange(List<TEntity> entities);
    Task<bool> Delete(TEntity entity);
    Task<bool> DeleteRange(List<TEntity> entities);
}