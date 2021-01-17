using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IMS.URM.BusinessServices.Abstractions
{
    public interface IActorSystem
    {
        Task<IActorHandle> GetOrCreate<TActor, TPayload>(string name)
            where TPayload : class where TActor : IActor<TPayload>;
    }
}
