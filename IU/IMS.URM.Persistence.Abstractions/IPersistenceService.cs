using IMS.URM.Entities.Abstractions;
using System.Threading.Tasks;

namespace IMS.URM.Persistence.Abstractions
{
    public interface IPersistenceService
    {
        ICollectionPersistenceService<T> Collection<T>(string name) where T : BaseEntity;
        Task<bool> CollectionExists(string name);
    }
}
