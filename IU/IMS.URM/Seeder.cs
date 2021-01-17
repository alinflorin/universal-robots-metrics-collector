using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IMS.URM.BusinessServices.Abstractions;
using IMS.URM.Entities;
using IMS.URM.Persistence.Abstractions;

namespace IMS.URM
{
    public static class Seeder
    {
        public static async Task Seed(IPersistenceService persistenceService, IPasswordEncryptor encrpytor)
        {
            if (!persistenceService.CollectionExists("robotevents").Result)
            {
                var col = persistenceService.Collection<RobotEvent>("robotevents");
                await col.CreateIndexes(new List<(Expression<Func<RobotEvent, object>> prop, IndexType type)>
                {
                    (x => x.EventDateTime, IndexType.Ascending),
                    (x => x.EventName, IndexType.Ascending),
                    (x => x.ReporterIp, IndexType.Ascending)
                });
            }

            if (!persistenceService.CollectionExists("users").Result)
            {
                var col = persistenceService.Collection<User>("users");
                await col.Save(new User
                {
                    IsAdmin = true,
                    Username = "admin",
                    Password = encrpytor.Encrypt("admin")
                });
            }
        }

    }
}