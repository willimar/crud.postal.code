using AutoMapper;
using postal.code.api.Models;
using postal.code.core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postal.code.api.Mapper
{
    public class AddressMapper : IMapperEntity
    {
        public void Mapper(IMapperConfigurationExpression profile)
        {
            profile.CreateMap<AddressModel, Address>()
                .ForMember(dest => dest.Id, map => map.MapFrom(source => Guid.NewGuid()))
                .ForMember(dest => dest.RegisterDate, map => map.MapFrom(source => Program.UtcNow))
                .ForMember(dest => dest.LastChangeDate, map => map.MapFrom(source => Program.UtcNow))
                .ForMember(dest => dest.PublicPlace, map => map.MapFrom(source => source.PublicPlace))
                .ForMember(dest => dest.StreetName, map => map.MapFrom(source => source.StreetName))
                .ForMember(dest => dest.FullStreetName, map => map.MapFrom(source => source.FullStreetName))
                .ForMember(dest => dest.PostalCode, map => map.MapFrom(source => source.PostalCode))
                .ForMember(dest => dest.City, map => map.MapFrom(source => new City() {
                    Id = Guid.NewGuid(),
                    RegisterDate = Program.UtcNow,
                    LastChangeDate = Program.UtcNow,
                    Name = source.CityName,
                    State = new State()
                    {
                        Id = Guid.NewGuid(),
                        RegisterDate = Program.UtcNow,
                        LastChangeDate = Program.UtcNow,
                        Name = source.StateName,
                        Initials = source.StateInitials,
                        Country = new Country()
                        {
                            Id = Guid.NewGuid(),
                            RegisterDate = Program.UtcNow,
                            LastChangeDate = Program.UtcNow,
                            Name = source.CountryName,
                            Initials = source.CountryInitials
                        }
                    }
                }));
        }
    }
}
