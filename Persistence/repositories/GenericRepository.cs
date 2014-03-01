using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging;
using System.Diagnostics;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Persistence.datacontext;
using System.Threading;
using System.Linq.Expressions;

namespace Persistence.repositories
{
    /// <summary>
    /// The EF-dependent, generic repository for data access
    /// </summary>
    /// <typeparam name="T">Type of entity for this Repository.</typeparam>
    public class GenericRepository<T>: IRepository<T> where T : class
    {
        protected IDataContext context { get; set; }

        private readonly Guid instanceId;
        protected DbSet<T> DbSet { get; set; }
        protected ILogger logger { get; set; }

        public GenericRepository(IDataContext dbContext, ILogger _logger)
        {
            if (dbContext == null)
                throw new ArgumentNullException("Null dbContext");

            context = dbContext;
            DbSet = context.Set<T>();
            instanceId = Guid.NewGuid();
            logger = _logger;
        }

        internal IQueryable<T> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        List<Expression<Func<T, object>>> includeProperties = null,
        int? page = null,
        int? pageSize = null)
        {
            IQueryable<T> query = DbSet;
            Stopwatch time = Stopwatch.StartNew();

            try
            {
                if (includeProperties != null)
                {
                    includeProperties.ForEach(i => query = query.Include(i));
                }

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                if (page != null && pageSize != null)
                {
                    query = query
                        .Skip((page.Value - 1) * pageSize.Value)
                        .Take(pageSize.Value);
                }
                time.Stop();
                logger.TraceApi("SQL Database", "RaceDataDBRepository.Get", time.Elapsed);
            }
            catch (Exception e)
            {
                logger.Error(e, "Error in RaceDataDBRepository.Get");
                throw;
            }
            return query;
        }

        internal async Task<IEnumerable<T>> GetAsync(
                    Expression<Func<T, bool>> filter = null,
                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                    List<Expression<Func<T, object>>> includeProperties = null,
                    int? page = null,
                    int? pageSize = null)
        {
            return Get(filter, orderBy, includeProperties, page, pageSize).AsEnumerable();
        }

        public T Find(int id)
        {
            T entity = null;
            Stopwatch time = Stopwatch.StartNew();

            try
            {
                entity = DbSet.Find(id);

                time.Stop();
                logger.TraceApi("SQL Database", "RaceDataDBRepository.Find", time.Elapsed, "id={0}", id);
            }
            catch (Exception e)
            {
                logger.Error(e, "Error in RaceDataDBRepository.Find(id={0})", id);
                throw;
            }

            return entity;
        }

        public async Task<T> FindAsync(params object[] keyValues)
        {
            T entity = null;
            Stopwatch time = Stopwatch.StartNew();

            try
            {
                entity = await DbSet.FindAsync(keyValues);

                time.Stop();
                logger.TraceApi("SQL Database", "RaceDataDBRepository.FindAsync", time.Elapsed, "id={0}");
            }
            catch (Exception e)
            {
                logger.Error(e, "Error in RaceDataDBRepository.FindAsync(id={0})");
                throw;
            }

            return entity;
        }

        public async Task<T> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            T entity = null;
            Stopwatch time = Stopwatch.StartNew();

            try
            {
                entity = await DbSet.FindAsync(cancellationToken, keyValues);

                time.Stop();
                logger.TraceApi("SQL Database", "RaceDataDBRepository.FindAsync", time.Elapsed);
            }
            catch (Exception e)
            {
                logger.Error(e, "Error in RaceDataDBRepository.FindAsync(id={0})");
                throw;
            }

            return entity;
        }

        public virtual IQueryable<T> SqlQuery(string query, params object[] parameters)
        {
            return DbSet.SqlQuery(query, parameters).AsQueryable();
        }

        public virtual IRepositoryQuery<T> Query()
        {
            var repositoryGetFluentHelper = new RepositoryQuery<T>(this);
            return repositoryGetFluentHelper;
        }

        public void Create(T entityToAdd)
        {
            Stopwatch time = Stopwatch.StartNew();

            //DbEntityEntry dbEntityEntry = context.Entry(entityToAdd);

            //if (dbEntityEntry.State != EntityState.Detached)
            //{
            //    dbEntityEntry.State = EntityState.Added;
            //}
            //else
            {
                try
                {
                    DbSet.Add(entityToAdd);
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error in RaceDataDBRepository.Create(entityToAdd={0})", entityToAdd);
                    throw;
                }
            }

            time.Stop();
            logger.TraceApi("SQL Database", "RaceDataDBRepository.Create", time.Elapsed, "entityToAdd={0}", entityToAdd);
        }

        public void Update(T entityToSave)
        {
            Stopwatch time = Stopwatch.StartNew();

            //DbEntityEntry dbEntityEntry = context.Entry(entityToSave);

            //if (dbEntityEntry.State == EntityState.Detached)
            {
                try
                {
                    DbSet.Attach(entityToSave);
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error in RaceDataDBRepository.Update(entityToSave={0})", entityToSave);
                    throw;
                }
            }
            context.SetModified(entityToSave);

            time.Stop();
            logger.TraceApi("SQL Database", "RaceDataDBRepository.Update", time.Elapsed, "entityToSave={0}", entityToSave);
        }

        public void Delete(T entityToDelete)
        {
            Stopwatch time = Stopwatch.StartNew();

            //DbEntityEntry dbEntityEntry = context.Entry(entityToDelete);

            //if (dbEntityEntry.State != EntityState.Deleted)
            //{
            //    dbEntityEntry.State = EntityState.Deleted;
            //}
            //else
            {
                try
                {
                    DbSet.Remove(entityToDelete);
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error in RaceDataDBRepository.Delete)");
                    throw;
                }
            }

            time.Stop();
            logger.TraceApi("SQL Database", "RaceDataDBRepository.Delete", time.Elapsed);
        }

        public void Delete(int id)
        {
            Stopwatch time = Stopwatch.StartNew();

            T entityToDelete = Find(id);

            if (entityToDelete == null) return;

            Delete(entityToDelete);

            time.Stop();
            logger.TraceApi("SQL Database", "RaceDataDBRepository.DeleteAsync", time.Elapsed, "id={0}", id);
        }

        public bool Exists(int id)
        {
            return DbSet.Count()>0;
        }

    }
}
