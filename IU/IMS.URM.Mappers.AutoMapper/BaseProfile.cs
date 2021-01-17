using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace IMS.URM.Mappers.AutoMapper
{
    public abstract class BaseProfile<T1, T2> : Profile
    {
        protected IMappingExpression<T1, T2> T1ToT2Config { get; set; }
        protected IMappingExpression<T2, T1> T2ToT1Config { get; set; }

        protected BaseProfile()
        {
            T1ToT2Config = CreateMap<T1, T2>().PreserveReferences();
            T2ToT1Config = T1ToT2Config.ReverseMap().PreserveReferences();
        }
    }
}
