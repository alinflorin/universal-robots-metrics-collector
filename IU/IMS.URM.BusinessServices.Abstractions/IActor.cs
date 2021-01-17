using System.Threading.Tasks;
using IMS.URM.Dto.Abstractions;

namespace IMS.URM.BusinessServices.Abstractions
{
    public interface IActor<in T> where T:class
    {
        Task OnReceive(T payload);
    }
}
