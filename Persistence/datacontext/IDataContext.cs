using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace Persistence.datacontext
{
    public interface IDataContext : IDisposable
    {
        void Setup();
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        void SetModified(object entity);
        DbEntityEntry Entry(object o);
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        //void Dispose();
    }
}
