using AutomataNETjuegos.Compilador.Excepciones;
using AutomataNETjuegos.Web.Models;
using System;

namespace AutomataNETjuegos.Web.MappingProfiles
{
    public class ErrorProfile : AutoMapper.Profile
    {
        public ErrorProfile()
        {
            CreateMap<Exception, ErrorModel>();

            CreateMap<string, ErrorModel>()
                .ForMember(x => x.Message, y => y.MapFrom(x => x));
        }
    }
}
