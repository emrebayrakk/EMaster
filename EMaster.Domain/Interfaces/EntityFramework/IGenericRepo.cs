using System.Linq.Expressions;

namespace EMaster.Domain.Interfaces.EntityFramework
{
    public interface IGenericRepo<TEntity, TEntityInput, TEntityOutput> where TEntity : class
    {
        int Add(TEntityInput entity);
        TEntity AddEntity(TEntityInput entity);
        TEntityOutput AddWithReturn(TEntityInput entityInput);
        int Update(TEntity entity);
        TEntity UpdateEntity(TEntityInput entity);
        int Delete(int id);
        TEntityOutput FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        List<TEntityOutput> GetAll(bool noTracking = true, params string[] includes);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
    }
}
