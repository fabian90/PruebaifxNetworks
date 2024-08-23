using Commons.Repository.Entities;
using Commons.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Commons.Repository.Repository
{
    /// <summary>
    ///  Generic repository class with standard CRUD methods.
    /// </summary>
    /// <typeparam name="TEntity">Database entity.</typeparam>
    /// <typeparam name="TContext">Database context. Class must inherit GenericContext.</typeparam>
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity> where TEntity : BaseEntity where TContext : DbContext
    {
        public readonly TContext _context;
        protected readonly DbSet<TEntity> _entity;

        /// <summary>
        /// Constructor Generic Repository 
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public GenericRepository(TContext context)
        {
            if (context == null)
                throw new ArgumentNullException("dbContext");

            _context = context;
            _entity = context.Set<TEntity>();
        }

        #region Adds

        /// <summary>
        /// Insert a new entity to our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Add(newEntity);</para>
        /// </summary>
        /// <param name="entity">Entity instance to be saved to our repository.</param>
        public async Task<TEntity> Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Method to insert a list of entities to our repository.
        /// <para>Examples:</para>
        /// <para>_repository.AddRange(entityList);</para>
        /// </summary>
        /// <param name="entities">List of entities to be saved to our repository.</param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete an entity from our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Delete(id);</para>
        /// </summary>
        /// <param name="entity">Identifier to remove</param>
        public async Task Delete(int id)
        {
            TEntity entity = await GetById(id);
            _entity.Remove(entity);
        }

        /// <summary>
        /// Delete an entity from our repository.
        /// <para>Examples:</para>
        /// <para>_repository.DeleteRange(entityList);</para>
        /// </summary>
        /// <param name="entities">List of entities to be deleted to our repository.</param>
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            _context.SaveChanges();
        }


        /// <summary>
        /// Delete an entity from our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Delete(p=> p.UserId == userId);</para>
        /// </summary>
        /// <param name="predicate">Filter applied to our search.</param>
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            _entity.AsQueryable()
                 .Where(predicate)
                 .ToList()
                 .ForEach(entity => _context.Remove(entity));
        }

        /// <summary>
        /// Delete an entity from our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Delete(entity);</para>
        /// </summary>
        public async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Gets

        /// <summary>
        /// Select all entities from our repository
        /// <para>Examples:</para>
        /// <para>_repository.GetAll();</para>
        /// </summary>
        /// <returns>Returns all entities from our repository.</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return _entity.AsEnumerable();
        }

        /// <summary>
        /// Find entitie for id
        /// </summary>
        /// <param name="id">number</param>
        /// <returns></returns>
        public async Task<TEntity> GetById(int id)
        {
            return await _entity.FindAsync(id);
        }

        /// <summary>
        /// Select an entity using it's primary keys as search criteria.
        /// <para>Examples:</para>
        /// <para>_repository.GetByKey(userId);</para>
        /// <para>_repository.GetByKey(companyId, userId);</para>
        /// </summary>
        /// <param name="primaryKeys">Primary key properties of our entity.</param>
        /// <returns>Returns an entity from our repository.</returns>
        public TEntity GetByKey(params object[] primaryKeys)
        {
            return _entity.Find(primaryKeys);
        }

        /// <summary>
        /// Select an entity from our repository using a filter.
        /// <para>Examples:</para>
        /// <para>_repository.Get(p=> p.UserId == 1);</para>
        /// <para>_repository.Get(p=> p.UserName.Contains("John") == true);</para>
        /// </summary>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns>Returns an entity from our repository.</returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _entity.Where(predicate).FirstOrDefault()!;
        }

        /// <summary>
        /// Select an entity from our repository using a filter.
        /// <para>Examples:</para>
        /// <para>_repository.GetList(p=> p.UserId == 1);</para>
        /// <para>_repository.GetList(p=> p.UserName.Contains("John") == true);</para>
        /// </summary>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns>Returns an entity from our repository.</returns>
        public List<TEntity> GetFilter(Expression<Func<TEntity, bool>> predicate)
        {
            return _entity.Where(predicate).ToList()!;
        }


        /// <summary>
        /// Method to verify if there are any entries in our repository using a filter.
        /// <para>Examples:</para>
        /// <para>_repository.Exists(p => p.UserId == user.Id)</para>
        /// <para>_repository.Exists(p => p.UserId == id && p.IsAdmin == false);</para>
        /// </summary>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns>Returns if any entity was found using the search criteria.</returns>
        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return _entity.Any(predicate);
        }

        #endregion

        #region Update

        /// <summary>
        /// Method to update our repository.
        /// </summary>
        /// <param name="entity">Entitie to be saved to our repository.</param>
        public async Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method to update our repository using a list of entities.
        /// <para>Examples:</para>
        /// <para>_repository.Update(entityList);</para>
        /// </summary>
        /// <param name="entities">List of entities to be saved to our repository.</param>
        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entity.UpdateRange(entities);
        }

        /// <summary>
        /// Method to update specific properties of an entity.
        /// <para>Examples:</para>
        /// <para>_repository.Update(user, p => p.FirstName, p => p.LastName);</para>
        /// <para>_repository.Update(user, p => p.Password);</para>
        /// </summary>
        /// <param name="entity">Entity instance to be saved to our repository.</param>
        /// <param name="propriedades">Array of expressions with the properties that will be changed.</param>
        public void UpdateProperties(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            _context.Attach(entity);

            foreach (var item in properties)
            {
                _context.Entry(entity).Property(item).IsModified = true;
            }
            _context.SaveChanges();
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Dispose method.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }


        #endregion
    }
}