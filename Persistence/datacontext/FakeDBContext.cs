using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

//https://genericunitofworkandrepositories.codeplex.com/SourceControl/latest#main/Repository/Providers/EntityFramework/Fakes/FakeDbContext.cs
namespace Persistence.datacontext
{
    public class FakeDBContext: IDataContext
    {
        private readonly Dictionary<Type, object> _fakeDbSets;

        protected FakeDBContext()
        {
            _fakeDbSets = new Dictionary<Type, object>();
        }

        public void Setup()
        {

        }

        public DbSet<T> Set<T>() where T : class
        {
            return (DbSet<T>)_fakeDbSets[typeof(T)];
        }

        public int SaveChanges()
        {
            return default(int);
        }

        public async Task<int> SaveChangesAsync()
        {
            //http://blog.stephencleary.com/2012/02/creating-tasks.html
            return await Task.Run(() => default(int));// new Task<int>(() => default(int));
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() => default(int));
        }

        public void AddFakeDbSet<TEntity,TFakeDbSet>() 
            where TEntity: class, new()
            where TFakeDbSet: FakeDBSet<TEntity>, IDbSet<TEntity>, new()
        {
            var fakeDbSet = Activator.CreateInstance<TFakeDbSet>();
            _fakeDbSets.Add(typeof(TEntity), fakeDbSet);
        }

        public void SetModified(object entity)
        {
            
        }

        public DbEntityEntry Entry(object o)
        {
            return null;
        }

        public IEnumerable<DbEntityValidationResult> GetValidationErrors()
        {
            return new List<DbEntityValidationResult>() { };
        }

        public void Dispose()
        {

        }

    }
}
