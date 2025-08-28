using AutoMapper; 
using SpendWise.Application.DTOs;
using SpendWise.Domain.Entities;

namespace SpendWise.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Usuario, UsuarioDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Valor));

        CreateMap<Categoria, CategoriaDto>();

        CreateMap<Transacao, TransacaoDto>()
            .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor.Valor))
            .ForMember(dest => dest.Moeda, opt => opt.MapFrom(src => src.Valor.Moeda))
            .ForMember(dest => dest.CategoriaNome, opt => opt.MapFrom(src => src.Categoria.Nome))
            .ForMember(dest => dest.UsuarioNome, opt => opt.MapFrom(src => src.Usuario.Nome));
    }
}
