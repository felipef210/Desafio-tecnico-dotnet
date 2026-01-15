using AutoMapper;
using GestaoDePacientesAPI.Dtos;
using GestaoDePacientesAPI.Models;

namespace GestaoDePacientesAPI.Profiles;

public class EnderecoProfile : Profile
{
    public EnderecoProfile()
    {
        CreateMap<ViaCepResponse, Endereco>()
            .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Localidade))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Uf))
            .ForMember(dest => dest.CEP, opt => opt.MapFrom(src => src.Cep));
    }
}
