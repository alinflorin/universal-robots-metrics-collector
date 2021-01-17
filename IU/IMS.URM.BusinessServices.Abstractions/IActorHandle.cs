using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.URM.BusinessServices.Abstractions
{
    public interface IActorHandle
    {
        void Send(object message);
    }
}
