using IMS.URM.Persistence.Abstractions;
using IMS.URM.Persistence.MongoDb;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DI
    {
        public static void AddUrmMongoDbPersistence(this IServiceCollection services)
        {
            services.AddSingleton<IPersistenceService, MongoDbPersistenceService>();
        }
    }
}
