using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Persistence.repositories
{
    public interface IRepository<T> where T : class 
    {
        T Find(int id);
        Task<T> FindAsync(params object[] keyValues);
        Task<T> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        void Create(T entityToAdd);
        void Update(T entityToSave);
        void Delete(int id);
        void Delete(T entityToDelete);
        bool Exists(int id);
        IQueryable<T> SqlQuery(string query, params object[] parameters);
        IRepositoryQuery<T> Query();
    }

}
