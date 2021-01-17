using IMS.URM.BusinessServices;
using IMS.URM.BusinessServices.Abstractions;
using IMS.URM.BusinessServices.Metrics;
using System.Collections.Generic;
using System.Linq;
using IMS.URM.BusinessServices.Actors;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DI
    {
        public static void AddUrmBusinessServices(this IServiceCollection services)
        {
            services.AddSingleton<IRobotEventsService, RobotEventsService>();
            services.AddSingleton<ITcpServerService, TcpServerService>();
            services.AddSingleton<ITcpDecoderService, TcpDecoderService>();
            services.AddSingleton<IPasswordEncryptor, Md5PasswordEncryptor>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IReflectionService, ReflectionService>();

            services.AddSingleton<IMetric, StandByMetric>();
            services.AddSingleton<IMetric, PartsMetric>();
            services.AddTransient<IList<IMetric>>(p => p.GetServices<IMetric>().ToList());

            services.AddTransient<RobotEventsActor>();
            services.AddSingleton<IActorSystem, ActorSystem>();
        }
    }
}
