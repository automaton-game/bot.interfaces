using AutomataNETjuegos.Compilador.Excepciones;
using Microsoft.CodeAnalysis;

namespace AutomataNETjuegos.Compilador.MappingProfiles
{
    public class ErrorCompilacionProfile : AutoMapper.Profile
    {
        public ErrorCompilacionProfile()
        {
            CreateMap<Diagnostic, ErrorCompilacion>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.Descripcion, y => y.MapFrom(x => x.GetMessage(null)));
        }
    }
}
