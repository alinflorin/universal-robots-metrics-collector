using IMS.URM.Entities.Abstractions;
using IMS.URM.Persistence.Abstractions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using IMS.URM.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace IMS.URM.Persistence.MongoDb
{
    public class MongoDbPersistenceService : IPersistenceService
    {
        private readonly IMongoDatabase _db;
        private const string MongoDbConnectionStringKey = "MongoDb";
        private const string MongoDbDbNameKey = "Persistence:DbName";

        public MongoDbPersistenceService(IConfiguration config)
        {
            ConfigureModel();

            var mongoClient = new MongoClient(config.GetConnectionString(MongoDbConnectionStringKey));
            _db = mongoClient.GetDatabase(config[MongoDbDbNameKey]);
        }

        public ICollectionPersistenceService<T> Collection<T>(string name) where T : BaseEntity
        {
            return new MongoDbCollectionPersistenceService<T>(_db.GetCollection<T>(name));
        }

        public async Task<bool> CollectionExists(string name)
        {
            return (await (await _db.ListCollectionNamesAsync()).ToListAsync()).Any(x => x.ToLower() == name.ToLower());
        }

        private static void ConfigureModel()
        {
            BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id).SetIdGenerator(ObjectIdGenerator.Instance);
            });
        }
    }
}
