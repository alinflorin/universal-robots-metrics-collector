using IMS.URM.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IMS.URM.Persistence.Abstractions
{
    public interface ICollectionPersistenceService<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expr);
        Task<IEnumerable<T>> Get(IEnumerable<Expression<Func<T, bool>>> whereExpressions = null, IEnumerable<(Expression<Func<T, object>>, bool)> orderByExpressions = null, int? limit = null);
        Task<T> Save(T entity);
        Task<string> CreateIndex(Expression<Func<T, object>> prop, IndexType type);
        Task<IEnumerable<string>> CreateIndexes(IEnumerable<(Expression<Func<T, object>> prop, IndexType type)> defs);
        Task<IEnumerable<TKey>> GetDistinct<TKey>(Expression<Func<T, TKey>> selector, Expression<Func<T, bool>> expr = null);
        Task<bool> Exists(Expression<Func<T, bool>> expr);
    }
}
