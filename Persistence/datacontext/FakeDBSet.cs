using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

//https://genericunitofworkandrepositories.codeplex.com/SourceControl/latest#main/Repository/Providers/EntityFramework/Fakes/FakeDbSet.cs
namespace Persistence.datacontext
{
    public abstract class FakeDBSet<TEntity> : DbSet<TEntity>, IDbSet<TEntity> where TEntity : class, new()
    {
        private readonly ObservableCollection<TEntity> _items;
        private readonly IQueryable _query;

        protected FakeDBSet()
        {
            _items = new ObservableCollection<TEntity>();
            _query = _items.AsQueryable();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        public IEnumerator<TEntity> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public Expression Expression
        {
            get { return _query.Expression; }
        }

        public Type ElementType
        {
            get { return _query.ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return _query.Provider; }
        }

        public override TEntity Add(TEntity entity)
        {
            _items.Add(entity);
            return entity;
        }

        public override TEntity Remove(TEntity entity)
        {
            _items.Remove(entity);
            return entity;
        }

        public override TEntity Attach(TEntity entity)
        {
            if (_items.Contains(entity))
            {
                _items.Remove(entity);
            }

            _items.Add(entity);

            return entity;
        }

        public override TEntity Create()
        {
            return new TEntity();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public override ObservableCollection<TEntity> Local
        {
            get { return _items; }
        }
    }
}
