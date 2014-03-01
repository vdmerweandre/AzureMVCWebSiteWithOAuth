using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;

//https://genericunitofworkandrepositories.codeplex.com/SourceControl/latest#releases/v2.0/Repository/RepositoryQuery.cs
namespace Persistence.repositories
{
    public interface IRepositoryQuery<T> where T : class
    {
        RepositoryQuery<T> Filter(Expression<Func<T, bool>> filter);
        RepositoryQuery<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
        RepositoryQuery<T> Include(Expression<Func<T, object>> expression);
        IEnumerable<T> GetPage(int page, int pageSize, out int totalCount);
        IQueryable<T> Get();
        Task<IEnumerable<T>> GetAsync();
        IQueryable<T> SqlQuery(string query, params object[] parameters);
    }
}
