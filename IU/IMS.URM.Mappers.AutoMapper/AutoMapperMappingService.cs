using AutoMapper;
using IMS.URM.Mappers.Abstractions;
using System;
using System.Threading.Tasks;

namespace IMS.URM.Mappers.AutoMapper
{
    public class AutoMapperMappingService : IMappingService
    {
        private readonly IMapper _mapper;

        public AutoMapperMappingService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<T2> Map<T1, T2>(T1 source) where T1 : class where T2 : class
        {
            return await Task.FromResult(_mapper.Map<T1, T2>(source));
        }

        public async Task Map<T1, T2>(T1 source, T2 destination)
            where T1 : class
            where T2 : class
        {
            await Task.FromResult(_mapper.Map(source, destination));
        }
    }
}
