using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using IMS.URM.BusinessServices.Abstractions;

namespace IMS.URM.BusinessServices
{
    public class AkkaActorHandle : IActorHandle
    {
        private readonly IActorRef _ref;

        public AkkaActorHandle(IActorRef reff)
        {
            _ref = reff;
        }

        public void Send(object message)
        {
            _ref.Tell(message);
        }
    }
}
