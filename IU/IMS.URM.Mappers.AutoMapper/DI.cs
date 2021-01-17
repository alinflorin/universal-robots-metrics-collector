using AutoMapper;
using IMS.URM.Mappers.Abstractions;
using IMS.URM.Mappers.AutoMapper;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DI
    {
        public static void AddUrmMappersAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperMappingService));
            services.AddSingleton<IMappingService, AutoMapperMappingService>();
        }
    }
}
