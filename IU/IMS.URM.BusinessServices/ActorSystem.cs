using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.Extensions.DependencyInjection;
using IMS.URM.BusinessServices.Abstractions;
using Microsoft.Extensions.Logging;

namespace IMS.URM.BusinessServices
{
    public class ActorSystem : IActorSystem
    {
        private readonly Akka.Actor.ActorSystem _actorSystem;
        private readonly IServiceProvider _services;
        private readonly ILogger<ActorSystem> _logger;

        public ActorSystem(IServiceProvider services, ILoggerFactory loggerFactory)
        {
            _services = services;
            _logger = loggerFactory.CreateLogger<ActorSystem>();
            _logger.LogInformation("Creating actor system");
            _actorSystem = Akka.Actor.ActorSystem.Create("ActorSystem");
            _actorSystem.UseServiceProvider(_services);
            _logger.LogInformation("Created actor system");
        }

        public async Task<IActorHandle> GetOrCreate<TActor, TPayload>(string name)
            where TActor : IActor<TPayload>
            where TPayload : class
        {
            IActorRef refer;
            try
            {
                _logger.LogInformation($"Trying to fetch actor {name}");
                refer = await _actorSystem.ActorSelection($"/user/{name}").ResolveOne(TimeSpan.FromSeconds(10));
                _logger.LogInformation($"Actor {name} fetched");
            }
            catch (ActorNotFoundException)
            {
                _logger.LogInformation($"Actor {name} not found, creating...");
                refer = _actorSystem.ActorOf(_actorSystem.DI().Props(typeof(TActor)), name);
                _logger.LogInformation($"Actor {name} created");
            }
            return new AkkaActorHandle(refer);
        }
    }
}
