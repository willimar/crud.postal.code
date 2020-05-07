using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postal.code.api.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
        }

        public IMapper CreateMapper(IMapperEntity mapperEntity)
        {
            var config = new MapperConfiguration(cfg => {
                mapperEntity.Mapper(cfg);
            });

            return config.CreateMapper();
        }

    }
}
