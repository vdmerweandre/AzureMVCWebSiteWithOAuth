using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

//https://genericunitofworkandrepositories.codeplex.com/SourceControl/latest#releases/v2.0/Repository/RepositoryQuery.cs
namespace Persistence.repositories
{
    public sealed class RepositoryQuery<T> : IRepositoryQuery<T> where T : class
    {
        private readonly List<Expression<Func<T, object>>> _includeProperties;
        private readonly GenericRepository<T> _repository;
        private Expression<Func<T, bool>> _filter;
        private Func<IQueryable<T>, IOrderedQueryable<T>> _orderByQuerable;
        private int? _page;
        private int? _pageSize;

        public RepositoryQuery(GenericRepository<T> repository)
        {
            _repository = repository;
            _includeProperties = new List<Expression<Func<T, object>>>();
        }

        public RepositoryQuery<T> Filter(Expression<Func<T, bool>> filter)
        {
            _filter = filter;
            return this;
        }

        public RepositoryQuery<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            _orderByQuerable = orderBy;
            return this;
        }

        public RepositoryQuery<T> Include(Expression<Func<T, object>> expression)
        {
            _includeProperties.Add(expression);
            return this;
        }

        public IEnumerable<T> GetPage(int page, int pageSize, out int totalCount)
        {
            _page = page;
            _pageSize = pageSize;
            totalCount = _repository.Get(_filter).Count();

            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _page, _pageSize);
        }

        public IQueryable<T> Get()
        {
            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _page, _pageSize);
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _repository.GetAsync(_filter, _orderByQuerable, _includeProperties, _page, _pageSize);
        }

        public IQueryable<T> SqlQuery(string query, params object[] parameters)
        {
            return _repository.SqlQuery(query, parameters).AsQueryable();
        }
    }
}
