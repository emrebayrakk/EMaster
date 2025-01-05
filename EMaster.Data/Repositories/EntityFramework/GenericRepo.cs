using EMaster.Data.Context;
using EMaster.Domain.Interfaces.EntityFramework;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EMaster.Data.Repositories.EntityFramework
{
    public class GenericRepo<TEntity, TEntityInput, TEntityOutput> : IGenericRepo<TEntity, TEntityInput, TEntityOutput> where TEntity : class
    {
        private readonly EMasterContext dbContext;

        public DbSet<TEntity> entity => dbContext.Set<TEntity>();

        public GenericRepo(DbContext dbContext)
        {
            this.dbContext = (EMasterContext?)(dbContext ?? throw new ArgumentNullException(nameof(dbContext)));
        }

        public int Add(TEntityInput entityInput)
        {
            try
            {
                var mappedEntity = entityInput.Adapt<TEntity>();
                this.entity.Add(mappedEntity);
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity AddEntity(TEntityInput entityInput)
        {
            try
            {
                var mappedEntity = entityInput.Adapt<TEntity>();
                this.entity.Add(mappedEntity);
                dbContext.SaveChanges();
                return mappedEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(long id)
        {
            try
            {
                var entity = this.entity.Find(id);
                if (entity is not null)
                {
                    this.entity.Remove(entity);
                    return dbContext.SaveChanges();
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntityOutput FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            try
            {
                var query = entity.AsQueryable();

                if (predicate != null)
                    query = query.Where(predicate);

                query = ApplyIncludes(query, includes);

                if (noTracking)
                    query = query.AsNoTracking();

                var entityResult = query.FirstOrDefaultAsync().Result;
                return entityResult.Adapt<TEntityOutput>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            try
            {
                var query = entity.AsQueryable();

                if (predicate != null)
                    query = query.Where(predicate);

                query = ApplyIncludes(query, includes);

                if (noTracking)
                    query = query.AsNoTracking();

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TEntityOutput> GetAll(bool noTracking = true, params string[] includes)
        {
            try
            {
                var query = noTracking ? entity.AsNoTracking() : entity;
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                var resultList = query.ToList();
                return resultList.Adapt<List<TEntityOutput>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(TEntity entity)
        {
            try
            {
                this.entity.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            try
            {
                if (includes != null)
                {
                    foreach (var includeItem in includes)
                    {
                        query = query.Include(includeItem);
                    }
                }

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity UpdateEntity(TEntityInput entityInput)
        {
            try
            {
                var mappedEntity = entityInput.Adapt<TEntity>();
                this.entity.Attach(mappedEntity);
                dbContext.Entry(mappedEntity).State = EntityState.Modified;

                var res = dbContext.SaveChanges();
                return res != -1 ? mappedEntity : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
