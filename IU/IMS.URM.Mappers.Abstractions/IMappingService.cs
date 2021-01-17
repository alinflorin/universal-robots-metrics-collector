using System.Threading.Tasks;

namespace IMS.URM.Mappers.Abstractions
{
    public interface IMappingService
    {
        Task<T2> Map<T1, T2>(T1 source) where T1 : class where T2 : class;
        Task Map<T1, T2>(T1 source, T2 destination) where T1 : class where T2 : class;
    }
}
