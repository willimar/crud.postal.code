using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postal.code.api.Mapper
{
    public interface IMapperEntity
    {
        void Mapper(IMapperConfigurationExpression profile);
    }
}
