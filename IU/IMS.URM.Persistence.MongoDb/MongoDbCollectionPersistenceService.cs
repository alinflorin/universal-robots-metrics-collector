using IMS.URM.Entities.Abstractions;
using IMS.URM.Persistence.Abstractions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IMS.URM.Persistence.MongoDb
{
    public class MongoDbCollectionPersistenceService<T> : ICollectionPersistenceService<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _col;

        public MongoDbCollectionPersistenceService(IMongoCollection<T> col)
        {
            _col = col;
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expr)
        {
            return await (await _col.FindAsync(expr)).ToListAsync();
        }

        public async Task<IEnumerable<T>> Get(IEnumerable<Expression<Func<T, bool>>> whereExpressions = null, IEnumerable<(Expression<Func<T, object>>, bool)> orderByExpressions = null, int? limit = null)
        {
            IQueryable<T> query = _col.AsQueryable();
            if (whereExpressions != null)
            {
                foreach (var we in whereExpressions)
                {
                    query = query.Where(we);
                }
            }
            if (orderByExpressions != null)
            {
                foreach (var oe in orderByExpressions)
                {
                    if (oe.Item2)
                    {
                        query = query.OrderBy(oe.Item1);
                    } else
                    {
                        query = query.OrderByDescending(oe.Item1);
                    }
                }
            }

            if (limit.HasValue)
            {
                query = query.Take(limit.Value);
            }
            var castedQuery = (IMongoQueryable<T>)query;
            return await castedQuery.ToListAsync();
        }

        public async Task<T> Save(T entity)
        {
            await _col.InsertOneAsync(entity);
            return entity;
        }

        public async Task<string> CreateIndex(Expression<Func<T, object>> prop, IndexType type)
        {
            IndexKeysDefinition<T> def;
            switch (type)
            {
                case IndexType.Ascending:
                    def = Builders<T>.IndexKeys.Ascending(prop);
                    break;
                case IndexType.Descending:
                    def = Builders<T>.IndexKeys.Descending(prop);
                    break;
                case IndexType.Hashed:
                    def = Builders<T>.IndexKeys.Hashed(prop);
                    break;
                case IndexType.Text:
                default:
                    def = Builders<T>.IndexKeys.Text(prop);
                    break;
            }

            var model = new CreateIndexModel<T>(def);
            return await _col.Indexes.CreateOneAsync(model);
        }

        public async Task<IEnumerable<string>> CreateIndexes(IEnumerable<(Expression<Func<T, object>> prop, IndexType type)> defs)
        {
            var models = defs
                .Select(x =>
                {
                    var (prop, type) = x;
                    IndexKeysDefinition<T> def;
                    switch (type)
                    {
                        case IndexType.Ascending:
                            def = Builders<T>.IndexKeys.Ascending(prop);
                            break;
                        case IndexType.Descending:
                            def = Builders<T>.IndexKeys.Descending(prop);
                            break;
                        case IndexType.Hashed:
                            def = Builders<T>.IndexKeys.Hashed(prop);
                            break;
                        default:
                            def = Builders<T>.IndexKeys.Text(prop);
                            break;
                    }

                    return new CreateIndexModel<T>(def);
                })
                .ToList();
            
            return await _col.Indexes.CreateManyAsync(models);
        }

        public async Task<IEnumerable<TKey>> GetDistinct<TKey>(Expression<Func<T, TKey>> selector, Expression<Func<T, bool>> expr = null)
        {
            IQueryable<T> query = _col.AsQueryable();
            if (expr != null)
            {
                query = query.Where(expr);
            }
            var continued = query.Select(selector).Distinct();
            var casted = (IMongoQueryable<TKey>)continued;
            return await casted.ToListAsync();
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> expr)
        {
            return (await _col.CountDocumentsAsync(expr)) > 0;
        }
    }
}
