using System;
using System.Threading.Tasks;
using Akka.Actor;
using IMS.URM.BusinessServices.Abstractions;
using IMS.URM.Dto.Abstractions;
using Microsoft.Extensions.Logging;

namespace IMS.URM.BusinessServices
{
    public abstract class Actor<T> : ReceiveActor, IActor<T> where T : class
    {
        protected readonly ILogger Logger;

        protected Actor(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger(GetType());
            ReceiveAsync<T>(async message =>
            {
                Logger.LogInformation("Actor received a message: {message}", message);
                try
                {
                    await OnReceive(message);
                }
                catch (Exception e)
                {
                    var actorName = Self.Path;
                    Logger.LogError(e, "Actor {actorName} exception when processing message {message}", actorName, message);
                }
            });
        }

        public abstract Task OnReceive(T payload);
    }
}
